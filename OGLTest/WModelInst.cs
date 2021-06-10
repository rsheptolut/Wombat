using MdxLib.Animator;
using MdxLib.Model;
using MdxLib.ModelFormats;
using MdxLib.Primitives;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WSequenceData
    {        
        public CSequence Sequence;
        public CGlobalSequence GlobalSequence;
        public int Start;
        public int End;
        public int Current;

        public WSequenceData(CSequence Sequence)
        {
            SetSequence(Sequence);
        }

        public WSequenceData(CGlobalSequence GlobalSequence)
        {
            SetSequence(GlobalSequence);
        }

        public WSequenceData()
        {

        }

        public CTime Time
        {
            get
            {
                if (GlobalSequence == null)
                    return new CTime(Current, Sequence);
                else
                    return new CTime(Current, GlobalSequence);
            }
        }

        private void Clear()
        {
            Sequence = null;
            GlobalSequence = null;
            Start = 0;
            End = 0;
            Current = 0;
        }

        public void SetSequence(CSequence Sequence)
        {
            Clear();
            this.Sequence = Sequence;
            this.Start = Sequence.IntervalStart;
            this.End = Sequence.IntervalEnd;
            this.Current = Start;
        }

        private void SetSequence(CGlobalSequence GlobalSequence)
        {
            Clear();
            this.GlobalSequence = GlobalSequence;
            this.Start = 0;
            this.End = GlobalSequence.Duration;
            this.Current = Start;
        }

        public void SetRandomTime()
        {
            Current = Start + WScene.Current.Rnd.Next(End - Start);
        }
        
        public bool Advance(double Time)
        {
            Current += (int)(Time * 960);
            if (Current < Start)
                Current = Start;
            if (Current > End)
            {
                if (Sequence == null || !Sequence.NonLooping)
                    Current = Start;
                else
                {
                    Current = End;
                    return false;
                }
            }
            return true;
        }
    }

    public class WModelInst
    {
        private const bool AnimThrottle = false;
        private List<WSequenceData> Sequences = new List<WSequenceData>();
        public bool IsAnimated;
        public List<WNodeInst> NodeInstances = new List<WNodeInst>();
        private List<WGeosetInst> Geosets = new List<WGeosetInst>();
        public static int MaxNodeInstances;
        double TotalTime;
        float AnimThreshold = (float)1 / (float)30;
        public WModelSource ModelSource;

        public WModelInst(WModelSource Model)
        {
            this.ModelSource = Model;

            foreach (INode Node in ModelSource.Model.Nodes)
                NodeInstances.Add(new WNodeInst(Node, Model.Model));

            foreach (var Node in NodeInstances)
                Node.Parent = NodeInstances.FirstOrDefault(Item => Item.Node == Node.Node.Parent.Node);

            NodeInstances.Sort((Item1, Item2) => Item1.IsChildTo(Item2));
            CopyGeosets();
            TotalTime = (float)WScene.Current.Rnd.NextDouble() * AnimThreshold;
            if (Model.Model.HasSequences)
                Sequences.Add(new WSequenceData(Model.Model.Sequences.First()));
            else
                Sequences.Add(new WSequenceData());
            foreach (var Sequence in Model.Model.GlobalSequences)
                Sequences.Add(new WSequenceData(Sequence));
            foreach (var Geoset in Geosets)
                Geoset.Update(null);
        }

        public void CopyGeosets()
        {
            foreach (var Geoset in ModelSource.Model.Geosets)
            {
                Geoset.Tag = 1;
                foreach (var Layer in Geoset.Material.Object.Layers)
                {
                    if (Layer.FilterMode == EMaterialLayerFilterMode.Transparent)
                        Geoset.Tag = 2;
                    if (Layer.FilterMode == EMaterialLayerFilterMode.Blend)
                        Geoset.Tag = 3;
                    if (Layer.FilterMode == EMaterialLayerFilterMode.Additive ||
                        Layer.FilterMode == EMaterialLayerFilterMode.AdditiveAlpha ||
                        Layer.FilterMode == EMaterialLayerFilterMode.Modulate)
                        Geoset.Tag = 4;
                }
            }

            for (int i = 1; i <= 4; i++)
            {
                foreach (var Geoset in ModelSource.Model.Geosets)
                {
                    if ((int)Geoset.Tag == i)
                        Geosets.Add(new WGeosetInst(this, Geoset));
                }
            }
        }

        public void StartAnimation(string AnimationName, bool RandomStart = false)
        {
            IsAnimated = !String.IsNullOrEmpty(AnimationName);
            if (IsAnimated)
            {
                Sequences[0].SetSequence(ModelSource.Model.Sequences.FirstOrDefault(Item => Item.Name == AnimationName));
                if (RandomStart)
                    Sequences[0].SetRandomTime();
            }
        }

        public void Update(double Time)
        {
            if (!IsAnimated)
                return;

            foreach (var Sequence in Sequences)
                Sequence.Advance(Time);
            CTime CurrentTime = new CTime(Sequences[0].Current, Sequences[0].Sequence);

            TotalTime += Time;

            if (AnimThrottle && TotalTime <= AnimThreshold)
                return;

            foreach (var Geoset in Geosets)
                Geoset.Update(CurrentTime);

            foreach (var Instance in NodeInstances)
                Instance.PrepareToTransform(
                    Sequences[Instance.TranslationGlobalSeqIndex].Time,
                    Sequences[Instance.RotationGlobalSeqIndex].Time,
                    Sequences[Instance.ScalingGlobalSeqIndex].Time);

            foreach (var Instance in NodeInstances)
                Instance.BuildTransformationMatrix(CurrentTime);

            foreach (var Instance in NodeInstances)
                if (Instance.Parent != null)
                    Instance.TransformationMatrix = Matrix4.Mult(Instance.TransformationMatrix, Instance.Parent.TransformationMatrix);

            TotalTime = 0;
        }

        public void Render()
        {
            foreach (var Geoset in Geosets)
                Geoset.Render();
        }
    }
}
