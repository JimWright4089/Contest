
#pragma hdrstop
//#include <condefs.h>

#include <fstream.h>
#include <iostream.h>
#include <string.h>
#include <stdlib.h>

//---------------------------------------------------------------------------
#pragma argsused

static void	ReadDir(ifstream &,char endchar);
static void	ReadLine(ifstream &in);
static void WriteLine(char *);

// Three global vars. One for the last line that was read, and two for holding
// the current status (i.e. the current data set and indentation width).
char buff[80];	// Buffer for lines
int indent = 0;	// Holds how much to indent at current pos
int dataset = 1;	// The current data set

int main(int argc, char **argv)
{
	ifstream	in("filemap.dat");
	if (in) {
		ReadLine(in);
    	while (buff[0] != '#') {
        	cout << "DATA SET " << dataset << ':' << endl;
            dataset++;
            cout << "ROOT" << endl;
	    	ReadDir(in,'*');
            ReadLine(in);
            cout << endl;	// seperate the data sets
        }
    }
	in.close();

    //char c;		// Since I compiled this as a console app, I have to read
    //cin >> c;	// something to keep the program from disappearing.

	return 0;
}

int mycmp(const void *a,const void *b)
{
	return strcmp((const char *)a,(const char *)b);
}

// Reads a dir from <in> and writes it to standard output, having the dirs in
// the dir in front of the files in the dir. The character that marks the end
// of the dir (either ']' or '*') is passed to the function, in order for us
// to be able to use it both for data sets and actual dirs.
static void	ReadDir(ifstream &in,char endchar)
{
	char	files[100][80];	// Holds the files in this dir. We assume there
    						// aren't more than 100 files in each dir.
    int		numfiles = 0;	// Number of files found so far

    indent++;
    while (buff[0] != endchar) {
        if (buff[0] == 'd') {
			WriteLine(buff);
			ReadLine(in);
        	ReadDir(in,']');
        }
        else {
        	strcpy(files[numfiles],buff);
            numfiles++;
        }
		ReadLine(in);
    }
    indent--;
    // Have now written all the dirs in this dir. Sort the files in this dir
    // and write them.
    qsort(files,numfiles,sizeof(files[0]),mycmp);
    for (int i=0 ; i<numfiles ; i++) WriteLine(files[i]);
}

// Helper func to read a line from <in> into the global buffer <buff>
static void ReadLine(ifstream &in)
{
	in.getline(buff,sizeof(buff));
}

// Helper func to write the string <s> to standard output, using the current
// indentation width. Each indentation is five spaces.
static void WriteLine(char *s)
{
   	for (int i=0 ; i<indent ; i++) cout << "|     ";
   	cout << s << endl;
}
