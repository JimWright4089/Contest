#include <iostream>
#include <iomanip>
#include <string>

using namespace std;
int gCuts = 0;

bool PaperCuts(string textFrom, string textTo)
{
    int size = textFrom.size();

    if (1 == size)
    {
        return true;
    }

    for (int i = size; i > 0; i--)
    {
        int n = size - i + 1;
        for (int j = 0; j < n;j++)
        {
            string newString = "";
            for (int k = 0; k < i; k++)
            {
                newString += textFrom.at(k+j);
            }

            int loc = textTo.find(newString,0);

            if (-1 != loc)
            {
                string newTo = "";
                if (0 == loc)
                {
                    string to2 = textTo.substr(loc + i);
                    string to3 = "";
                    for (int k = 0; k < i; k++)
                    {
                        to3 += ".";
                    }
                    newTo = to3 + to2;
                }
                else
                {
                    string to1 = textTo.substr(0,loc);
                    string to2 = textTo.substr(loc+i);
                    string to3 = "";
                    for (int k = 0; k < i;k++)
                    {
                        to3 += ".";
                    }
                    newTo = to1 + to3 + to2;
                }

                string from1 = "";
                string from2 = "";

                if (0 == j)
                {
                    if(i!=size)
                    {
                        from2 = textFrom.substr(i);
                        if (true == PaperCuts(from2, newTo))
                        {
                            gCuts++;
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (n-1 == j)
                    {
                        from2 = textFrom.substr(0,j);
                        if (true == PaperCuts(from2, newTo))
                        {
                            gCuts++;
                            return true;
                        }
                    }
                    else
                    {
                        from1 = textFrom.substr(0, j);
                        from2 = textFrom.substr(i+j);
                        if ((true == PaperCuts(from1, newTo)) &&
                            (true == PaperCuts(from2, newTo)))
                        {
                            gCuts += 2;
                            return true;
                        }
                    }

                }
            }
        }
    }
    return false;
}


int main() {
    string textFrom = "";
    string textTo = "";
    std::cin >> textFrom >> textTo;
    PaperCuts(textFrom,textTo);
    std::cout << gCuts << std::endl;
}