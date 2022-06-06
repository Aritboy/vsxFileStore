#pragma once
#include <windows.h>
#include <iostream>

class MapplingTestClass
{
public:
	HANDLE hFileMapping;
	LPVOID pViewOfFile;
	HANDLE hFileMapping1;
	LPVOID pViewOfFile1;
	MapplingTestClass() {
		//--------- �������� �����, ������������� � ������ --------------------------------
		hFileMapping = CreateFileMappingA(
			INVALID_HANDLE_VALUE, // �� ��������� � ������ �� �����
			NULL,                           		// ����������� �� �������������
			PAGE_READWRITE,    	// ��� �������
			0,                              		// ������� 4 ����� ������� �����
			1000,					// ������ 4 ����� �������
			"Example MMF Object");               // name
		// �������� ����� ������ ����� ����������� ������, ����������� ������, ������������ � ������
		pViewOfFile = MapViewOfFile(
			hFileMapping,             // handle to file-mapping object
			FILE_MAP_ALL_ACCESS,        // desired access
			0,
			0,
			0);                         // map all file
	//--------- �������� �����, ������������� � ������ --------------------------------
		hFileMapping1 = CreateFileMappingA(
			INVALID_HANDLE_VALUE, // �� ��������� � ������ �� �����
			NULL,                           		// ����������� �� �������������
			PAGE_READWRITE,    	// ��� �������
			0,                              		// ������� 4 ����� ������� �����
			1000,					// ������ 4 ����� �������
			"Example MMF Object");               // name
		// �������� ����� ������ ����� ����������� ������, ����������� ������, ������������ � ������
		pViewOfFile1 = MapViewOfFile(
			hFileMapping,             // handle to file-mapping object
			FILE_MAP_ALL_ACCESS,        // desired access
			0,
			0,
			0);
	};
	void MapplingClassOffFile() {
		//-------- �������� �����, ������������� � ������ ---------------
		UnmapViewOfFile(pViewOfFile);
		CloseHandle(hFileMapping);
		//-------- �������� �����, ������������� � ������ ---------------
		UnmapViewOfFile(pViewOfFile1);
		CloseHandle(hFileMapping1);
	};
};

