//+-----------------------------------------------------------------------------
//|
//| Copyright (C) 2008, Magnus Ostberg, aka Magos
//| Contact: MagosX@GMail.com, http://www.magosx.com
//|
//| This file is part of MdxLib.
//| MdxLib is a library to manipulate (load, modify, save) models for the
//| game WarCraft 3. It can (and is supposed to) be freely used in tools
//| and programs made by other developers.
//|
//| WarCraft is a trademark of Blizzard Entertainment, Inc.
//|
//| MdxLib is free software: you can redistribute it and/or modify
//| it under the terms of the GNU General Public License as published by
//| the Free Software Foundation, either version 3 of the License, or
//| (at your option) any later version.
//|
//| MdxLib is distributed in the hope that it will be useful,
//| but WITHOUT ANY WARRANTY; without even the implied warranty of
//| MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//| GNU General Public License for more details.
//|
//| You should have received a copy of the GNU General Public License
//| along with MdxLib. If not, see <http://www.gnu.org/licenses/>.
//|
//| This header must remain unaltered if changes are made to the file.
//| Additional information may be added as needed.
//|
//+-----------------------------------------------------------------------------
namespace MdxLib.ModelFormats.Mdl
{
	internal sealed class CLoader
	{
		public CLoader(string Name, System.IO.Stream Stream)
		{
			_Name = Name;
			_Attacher = new Attacher.CAttacherContainer();

			AppendPattern("newline", "\\n");
			AppendPattern("whitespace", "[\\t\\r ]+");
			AppendPattern("metacomment", "//\\<\\?xml(.|\\n)*?//\\</meta\\>");
			AppendPattern("comment", "//.*?\\n|/\\*(.|\\n)*?\\*/");
			AppendPattern("colon", ":");
			AppendPattern("separator", ",");
			AppendPattern("word", "[a-zA-Z_][a-zA-Z0-9_]*");
			AppendPattern("float", "[+-]?\\d*\\.\\d+[Ee][+-]?\\d+|[+-]?\\d+[Ee][+-]?\\d+|[+-]?\\d*\\.\\d+");
			AppendPattern("integer", "[+-]?\\d+");
			AppendPattern("string", "\"([^\"\n\\\\]|\\\\.)*?\"");
			AppendPattern("bracket_rl", "\\(");
			AppendPattern("bracket_rr", "\\)");
			AppendPattern("bracket_sl", "\\[");
			AppendPattern("bracket_sr", "\\]");
			AppendPattern("bracket_al", "\\<");
			AppendPattern("bracket_ar", "\\>");
			AppendPattern("bracket_cl", "{");
			AppendPattern("bracket_cr", "}");
			AppendPattern("unknown", ".+?");

			Expression = new System.Text.RegularExpressions.Regex(ExpressionString);

			using(System.IO.StreamReader Reader = new System.IO.StreamReader(Stream, CConstants.TextEncoding, true))
			{
				InputString = Reader.ReadToEnd();
			}
		}

		public Token.EType ReadToken()
		{
			ReadNextToken();

			return CurrentType;
		}

		public string ReadMetaData()
		{
			ReadNextToken();

			if(CurrentType != Token.EType.MetaComment) throw new System.Exception("Syntax error at line " + Line + ", expected a meta comment, got \"" + CurrentGroup.Value + "\"!");

			System.Text.StringBuilder MetaData = new System.Text.StringBuilder();
			string[] MetaLines = CurrentGroup.Value.Split(new string[] { "\n" }, System.StringSplitOptions.None);

			foreach(string MetaLine in MetaLines)
			{
				string NewMetaLine = MetaLine.Trim();
				if(NewMetaLine.StartsWith("//")) NewMetaLine = NewMetaLine.Remove(0, 2);
				MetaData.Append(NewMetaLine.Trim());
			}

			return MetaData.ToString();
		}

		public string ReadWord()
		{
			ReadNextToken();

			if(CurrentType != Token.EType.Word) throw new System.Exception("Syntax error at line " + Line + ", expected a word, got \"" + CurrentGroup.Value + "\"!");

			return CurrentGroup.Value.ToLower();
		}

		public int ReadId()
		{
			ReadNextToken();

			switch(CurrentType)
			{
				case Token.EType.Word:
				{
					string Id = CurrentGroup.Value.ToLower();

					switch(Id)
					{
						case "none":
						case "multiple":
						{
							return CConstants.InvalidId;
						}
					}

					throw new System.Exception("Syntax error at line " + Line + ", unknown ID \"" + Id + "\"!");
				}

				case Token.EType.Integer:
				{
					return int.Parse(CurrentGroup.Value);
				}
			}

			throw new System.Exception("Syntax error at line " + Line + ", expected an ID, got \"" + CurrentGroup.Value + "\"!");
		}

		public int ReadInteger()
		{
			ReadNextToken();

			if(CurrentType != Token.EType.Integer) throw new System.Exception("Syntax error at line " + Line + ", expected an integer, got \"" + CurrentGroup.Value + "\"!");

			return int.Parse(CurrentGroup.Value);
		}

		public float ReadFloat()
		{
			ReadNextToken();

			if((CurrentType != Token.EType.Integer) && (CurrentType != Token.EType.Float)) throw new System.Exception("Syntax error at line " + Line + ", expected a float, got \"" + CurrentGroup.Value + "\"!");

			return float.Parse(CurrentGroup.Value, CConstants.NumberFormat);
		}

		public string ReadString()
		{
			ReadNextToken();

			if(CurrentType != Token.EType.String) throw new System.Exception("Syntax error at line " + Line + ", expected a string, got \"" + CurrentGroup.Value + "\"!");

			string String = CurrentGroup.Value;

			if(String.StartsWith("\"")) String = String.Remove(0, 1);
			if(String.EndsWith("\"")) String = String.Remove(String.Length - 1, 1);

			return String.Replace("\\\"", "\"");
		}

		public Primitives.CVector2 ReadVector2()
		{
			ExpectToken(Token.EType.CurlyBracketLeft);
			float X = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float Y = ReadFloat();
			ExpectToken(Token.EType.CurlyBracketRight);

			return new Primitives.CVector2(X, Y);
		}

		public Primitives.CVector3 ReadVector3()
		{
			ExpectToken(Token.EType.CurlyBracketLeft);
			float X = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float Y = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float Z = ReadFloat();
			ExpectToken(Token.EType.CurlyBracketRight);

			return new Primitives.CVector3(X, Y, Z);
		}

		public Primitives.CVector4 ReadVector4()
		{
			ExpectToken(Token.EType.CurlyBracketLeft);
			float X = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float Y = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float Z = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float W = ReadFloat();
			ExpectToken(Token.EType.CurlyBracketRight);

			return new Primitives.CVector4(X, Y, Z, W);
		}

		public Primitives.CVector3 ReadColor()
		{
			ExpectToken(Token.EType.CurlyBracketLeft);
			float B = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float G = ReadFloat();
			ExpectToken(Token.EType.Separator);
			float R = ReadFloat();
			ExpectToken(Token.EType.CurlyBracketRight);

			return new Primitives.CVector3(R, G, B);
		}

		public void ExpectToken(Token.EType ExpectedType)
		{
			Token.EType Type = ReadToken();

			if(Type != ExpectedType) throw new System.Exception("Syntax error at line " + Line + ", expected " + TokenToText(Type) + ", got \"" + CurrentGroup.Value + "\"!");
		}

		public void ExpectWord(string ExpectedWord)
		{
			string Word = ReadWord();

			if(Word != ExpectedWord.ToLower()) throw new System.Exception("Syntax error at line " + Line + ", expected \"" + ExpectedWord + "\", got \"" + Word + "\"!");
		}

		public Token.EType PeekToken()
		{
			PeekNextToken();

			return CurrentType;
		}

		private void ReadNextToken()
		{
			if(!ParseNextToken()) throw new System.Exception("Unexpected EOF reached at line " + Line + "!");
		}

		private void PeekNextToken()
		{
			if(!ParseNextToken()) throw new System.Exception("Unexpected EOF reached at line " + Line + "!");
			AlreadyParsed = true;
		}

		private bool ParseNextToken()
		{
			if(AlreadyParsed)
			{
				AlreadyParsed = false;
				return true;
			}

			while(true)
			{
				CurrentMatch = (CurrentMatch != null) ? CurrentMatch.NextMatch() : Expression.Match(InputString);
				if(!CurrentMatch.Success)
				{
					_Eof = true;
					break;
				}

				for(int Index = FirstGroupIndex; Index < CurrentMatch.Groups.Count; Index++)
				{
					CurrentGroup = CurrentMatch.Groups[Index];
					if(!CurrentGroup.Success) continue;

					CurrentType = PatternNameToType(Expression.GroupNameFromNumber(Index));

					switch(CurrentType)
					{
						case Token.EType.NewLine:
						{
							_Line++;
							break;
						}

						case Token.EType.WhiteSpace:
						{
							break;
						}

						case Token.EType.MetaComment:
						{
							foreach(char Character in CurrentGroup.Value)
							{
								if(Character == '\n') _Line++;
							}

							return true;
						}

						case Token.EType.Comment:
						{
							foreach(char Character in CurrentGroup.Value)
							{
								if(Character == '\n') _Line++;
							}

							break;
						}

						case Token.EType.Unknown:
						{
							throw new System.Exception("Syntax error at line " + Line + ", unknown token \"" + CurrentGroup.Value + "\"!");
						}

						default:
						{
							return true;
						}
					}

					break;
				}
			}

			CurrentType = Token.EType.Unknown;
			CurrentMatch = null;
			CurrentGroup = null;

			return false;
		}

		private void AppendPattern(string Name, string Pattern)
		{
			if(ExpressionString != "") ExpressionString += "|";
			ExpressionString += "(?<" + Name + ">" + Pattern + ")";
		}

		private Token.EType PatternNameToType(string Name)
		{
			switch(Name)
			{
				case "newline": return Token.EType.NewLine;
				case "whitespace": return Token.EType.WhiteSpace;
				case "metacomment": return Token.EType.MetaComment;
				case "comment": return Token.EType.Comment;
				case "colon": return Token.EType.Colon;
				case "separator": return Token.EType.Separator;
				case "word": return Token.EType.Word;
				case "float": return Token.EType.Float;
				case "integer": return Token.EType.Integer;
				case "string": return Token.EType.String;
				case "bracket_rl": return Token.EType.RoundBracketLeft;
				case "bracket_rr": return Token.EType.RoundBracketRight;
				case "bracket_sl": return Token.EType.SquareBracketLeft;
				case "bracket_sr": return Token.EType.SquareBracketRight;
				case "bracket_al": return Token.EType.AngleBracketLeft;
				case "bracket_ar": return Token.EType.AngleBracketRight;
				case "bracket_cl": return Token.EType.CurlyBracketLeft;
				case "bracket_cr": return Token.EType.CurlyBracketRight;
				case "unknown": return Token.EType.Unknown;
			}

			return Token.EType.Unknown;
		}

		private string TokenToText(Token.EType Type)
		{
			switch(Type)
			{
				case Token.EType.NewLine: return "a newline";
				case Token.EType.WhiteSpace: return "whitespace";
				case Token.EType.MetaComment: return "a meta comment";
				case Token.EType.Comment: return "a comment";
				case Token.EType.Colon: return "\":\"";
				case Token.EType.Separator: return "\",\"";
				case Token.EType.Word: return "a word";
				case Token.EType.Float: return "a float";
				case Token.EType.Integer: return "an integer";
				case Token.EType.String: return "a string";
				case Token.EType.RoundBracketLeft: return "\"(\"";
				case Token.EType.RoundBracketRight: return "\")\"";
				case Token.EType.SquareBracketLeft: return "\"[\"";
				case Token.EType.SquareBracketRight: return "\"]\"";
				case Token.EType.AngleBracketLeft: return "\"<\"";
				case Token.EType.AngleBracketRight: return "\">\"";
				case Token.EType.CurlyBracketLeft: return "\"{\"";
				case Token.EType.CurlyBracketRight: return "\"}\"";
				case Token.EType.Unknown: return "<unknown>";
			}

			return "<unknown>";
		}

		public int Line
		{
			get
			{
				return _Line;
			}
		}

		public string Name
		{
			get
			{
				return _Name;
			}
		}

		public bool Eof
		{
			get
			{
				return _Eof;
			}
		}

		public Attacher.CAttacherContainer Attacher
		{
			get
			{
				return _Attacher;
			}
		}

		
		private int _Line = 1;
		private string _Name = "";
		private bool _Eof = false;
		private Attacher.CAttacherContainer _Attacher = null;

		private string InputString = "";
		private string ExpressionString = "";
		private System.Text.RegularExpressions.Regex Expression = null;

		private bool AlreadyParsed = false;
		private Token.EType CurrentType = Token.EType.Unknown;
		private System.Text.RegularExpressions.Match CurrentMatch = null;
		private System.Text.RegularExpressions.Group CurrentGroup = null;

		private const int FirstGroupIndex = 4;
	}
}
