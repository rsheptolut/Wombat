//+-----------------------------------------------------------------------------
//| Included files
//+-----------------------------------------------------------------------------
#include "TokenOutStream.h"


//+-----------------------------------------------------------------------------
//| Constructor
//+-----------------------------------------------------------------------------
TOKEN_OUT_STREAM::TOKEN_OUT_STREAM()
{
	FileName = "";
}


//+-----------------------------------------------------------------------------
//| Destructor
//+-----------------------------------------------------------------------------
TOKEN_OUT_STREAM::~TOKEN_OUT_STREAM()
{
	Clear();
}


//+-----------------------------------------------------------------------------
//| Sets a new filename
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::SetFileName(CONST std::string& NewFileName)
{
	FileName = NewFileName;
}


//+-----------------------------------------------------------------------------
//| Returns the filename
//+-----------------------------------------------------------------------------
std::string TOKEN_OUT_STREAM::GetFileName()
{
	return FileName;
}


//+-----------------------------------------------------------------------------
//| Clears the token stream
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::Clear()
{
	Stream.str("");
	Stream.clear();
}


//+-----------------------------------------------------------------------------
//| Saves a file
//+-----------------------------------------------------------------------------
BOOL TOKEN_OUT_STREAM::Save(BUFFER& Buffer)
{
	if(!Buffer.Resize(static_cast<INT>(Stream.str().size())))
	{
		Error.SetMessage("Unable to save \"" + FileName + "\", buffer resize failed!");
		return FALSE;
	}

	std::memcpy(Buffer.GetData(), Stream.str().c_str(), static_cast<INT>(Stream.str().size()));

	return TRUE;
}


//+-----------------------------------------------------------------------------
//| Writes a boolean
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteBool(BOOL Bool)
{
	Stream << (Bool ? "True" : "False");
}


//+-----------------------------------------------------------------------------
//| Writes a character
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteChar(CHAR Char)
{
	Stream << Char;
}


//+-----------------------------------------------------------------------------
//| Writes an integer
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteInt(INT Int)
{
	Stream << Int;
}


//+-----------------------------------------------------------------------------
//| Writes a floating point number
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteFloat(FLOAT Float)
{
	Stream << Float;
}


//+-----------------------------------------------------------------------------
//| Writes a double precision floating point number
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteDouble(DOUBLE Double)
{
	Stream << Double;
}


//+-----------------------------------------------------------------------------
//| Writes a word
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteWord(CONST std::string& Word)
{
	Stream << Word;
}


//+-----------------------------------------------------------------------------
//| Writes a line
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteLine(CONST std::string& Line)
{
	Stream << Line << "\r\n";
}


//+-----------------------------------------------------------------------------
//| Writes a string
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteString(CONST std::string& String)
{
	Stream << "\"" << String << "\"";
}


//+-----------------------------------------------------------------------------
//| Writes a 2-dimensional vector
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteVector2(CONST D3DXVECTOR2& Vector)
{
	Stream << "{ " << Vector.x << ", " << Vector.y << " }";
}


//+-----------------------------------------------------------------------------
//| Writes a 3-dimensional vector
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteVector3(CONST D3DXVECTOR3& Vector)
{
	Stream << "{ " << Vector.x << ", " << Vector.y << ", " << Vector.z << " }";
}


//+-----------------------------------------------------------------------------
//| Writes a 4-dimensional vector
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteVector4(CONST D3DXVECTOR4& Vector)
{
	Stream << "{ " << Vector.x << ", " << Vector.y << ", " << Vector.z << ", " << Vector.w << " }";
}


//+-----------------------------------------------------------------------------
//| Writes a 2-dimensional vector
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteVector2(CONST D3DXVECTOR4& Vector)
{
	Stream << "{ " << Vector.x << ", " << Vector.y << " }";
}


//+-----------------------------------------------------------------------------
//| Writes a 3-dimensional vector
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteVector3(CONST D3DXVECTOR4& Vector)
{
	Stream << "{ " << Vector.x << ", " << Vector.y << ", " << Vector.z << " }";
}


//+-----------------------------------------------------------------------------
//| Writes a comment header
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteHeader(CONST std::string& Title)
{
	Stream << "//+-----------------------------------------------------------------------------\r\n";
	Stream << "//|" << Title << "\r\n";
	Stream << "//+-----------------------------------------------------------------------------\r\n";
}


//+-----------------------------------------------------------------------------
//| Writes a line break
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteBreak(INT NrOfBreaks)
{
	while(NrOfBreaks > 0)
	{
		Stream << "\r\n";
		NrOfBreaks--;
	}
}


//+-----------------------------------------------------------------------------
//| Writes a format tab
//+-----------------------------------------------------------------------------
VOID TOKEN_OUT_STREAM::WriteTab(INT NrOfTabs)
{
	while(NrOfTabs > 0)
	{
		Stream << "\t";
		NrOfTabs--;
	}
}
