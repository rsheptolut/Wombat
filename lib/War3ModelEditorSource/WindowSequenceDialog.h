//+-----------------------------------------------------------------------------
//| Inclusion guard
//+-----------------------------------------------------------------------------
#ifndef MAGOS_WINDOW_SEQUENCE_DIALOG_H
#define MAGOS_WINDOW_SEQUENCE_DIALOG_H


//+-----------------------------------------------------------------------------
//| Included files
//+-----------------------------------------------------------------------------
#include "WindowDialog.h"
#include "Model.h"


//+-----------------------------------------------------------------------------
//| Sequence dialog window class
//+-----------------------------------------------------------------------------
class WINDOW_SEQUENCE_DIALOG : public WINDOW_DIALOG
{
	public:
		CONSTRUCTOR WINDOW_SEQUENCE_DIALOG();
		DESTRUCTOR ~WINDOW_SEQUENCE_DIALOG();

		BOOL Display(HWND ParentWindow, MODEL_SEQUENCE_DATA& Data) CONST;

	protected:
		static BOOL CALLBACK DialogMessageHandler(HWND Window, UINT Message, WPARAM W, LPARAM L);

		static MODEL_SEQUENCE_DATA StaticData;
};


//+-----------------------------------------------------------------------------
//| Global objects
//+-----------------------------------------------------------------------------
extern WINDOW_SEQUENCE_DIALOG SequenceDialog;


//+-----------------------------------------------------------------------------
//| End of inclusion guard
//+-----------------------------------------------------------------------------
#endif
