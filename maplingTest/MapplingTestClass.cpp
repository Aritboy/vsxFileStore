#include "MapplingTestClass.h"
#include <windows.h>
#include <iostream>

int main()
{
	MapplingTestClass test;
	const char* s = "Hd";
	strcpy_s((char*)test.pViewOfFile, strlen(s)+1, s); // записать строку УHello WorldФ
	std::cout << (char*)test.pViewOfFile;
	test.hFileMapping;
	MessageBox(NULL, test.hFileMapping1, TEXT("Process2"), MB_OK);
	//---------------------------------------------------------------
	system("pause");
	return false;
}