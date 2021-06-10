//+-----------------------------------------------------------------------------
//| Included files
//+-----------------------------------------------------------------------------
#include "ModelHelper.h"


//+-----------------------------------------------------------------------------
//| Constructor
//+-----------------------------------------------------------------------------
MODEL_HELPER::MODEL_HELPER()
{
	ModelBaseData = &HelperData;
	ModelBaseData->Type = NODE_TYPE_HELPER;
}


//+-----------------------------------------------------------------------------
//| Destructor
//+-----------------------------------------------------------------------------
MODEL_HELPER::~MODEL_HELPER()
{
	Clear();
}


//+-----------------------------------------------------------------------------
//| Clears the helper
//+-----------------------------------------------------------------------------
VOID MODEL_HELPER::Clear()
{
	HelperData = MODEL_HELPER_DATA();
}


//+-----------------------------------------------------------------------------
//| Returns the mdx size of the helper
//+-----------------------------------------------------------------------------
INT MODEL_HELPER::GetSize()
{
	return GetBaseSize();
}


//+-----------------------------------------------------------------------------
//| Returns a reference to the data
//+-----------------------------------------------------------------------------
MODEL_HELPER_DATA& MODEL_HELPER::Data()
{
	return HelperData;
}
