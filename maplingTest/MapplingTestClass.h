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
		//--------- создание файла, проецируемого в память --------------------------------
		hFileMapping = CreateFileMappingA(
			INVALID_HANDLE_VALUE, // не связывать с файлом на диске
			NULL,                           		// неограничен по пользователям
			PAGE_READWRITE,    	// вид доступа
			0,                              		// верхние 4 байта размера файла
			1000,					// нижние 4 байта размера
			"Example MMF Object");               // name
		// получить адрес начала блока оперативной памяти, занимаемого файлом, проецируемым в память
		pViewOfFile = MapViewOfFile(
			hFileMapping,             // handle to file-mapping object
			FILE_MAP_ALL_ACCESS,        // desired access
			0,
			0,
			0);                         // map all file
	//--------- создание файла, проецируемого в память --------------------------------
		hFileMapping1 = CreateFileMappingA(
			INVALID_HANDLE_VALUE, // не связывать с файлом на диске
			NULL,                           		// неограничен по пользователям
			PAGE_READWRITE,    	// вид доступа
			0,                              		// верхние 4 байта размера файла
			1000,					// нижние 4 байта размера
			"Example MMF Object");               // name
		// получить адрес начала блока оперативной памяти, занимаемого файлом, проецируемым в память
		pViewOfFile1 = MapViewOfFile(
			hFileMapping,             // handle to file-mapping object
			FILE_MAP_ALL_ACCESS,        // desired access
			0,
			0,
			0);
	};
	void MapplingClassOffFile() {
		//-------- закрытие файла, проецируемого в память ---------------
		UnmapViewOfFile(pViewOfFile);
		CloseHandle(hFileMapping);
		//-------- закрытие файла, проецируемого в память ---------------
		UnmapViewOfFile(pViewOfFile1);
		CloseHandle(hFileMapping1);
	};
};

