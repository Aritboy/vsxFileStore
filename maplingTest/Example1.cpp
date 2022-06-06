#include <stdio.h>
#include <tchar.h>

void f_write();

int maina()
{
	FILE* f;
	fopen_s(&f, "f1.dat", "wb");
	int x[10] = { 0,1,2,3,4 };
	fwrite(x, sizeof(int), 10, f);
	fclose(f);

	f_write();


	return 0;
}

void f_write()
{
	FILE *f;
	fopen_s(&f, "f5.dat", "wb");
	int x[8] = { 0,1,2,3,4,5,6,7 };
	fwrite(x, sizeof(int), 8, f);
	fclose(f);
}