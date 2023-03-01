#include <iostream>
#include <iomanip>
#include <string>
#include <algorithm>
#include <math.h>

using namespace std;

const int MAX_NUM = 10;
const int MIN_DICE = 2;
const int MAX_DICE = 12;

double maxT = 0;
double minT = 0;
string minO = "-1";
string maxO = "-1";

int sitem[]{ 0,1,3,5,8,12,17,23,31,40,50 };
int eitem[]{ 1,3,5,8,12,17,23,31,40,50,62 };

string sboard[] {"2",
                 "3","21",
                 "4","31",
                 "5","41","32",
                 "6","51","42","321",
                 "7","61","52","43","421",
                 "8","71","62","53","521","431",
                 "9","81","72","63","621","54","531","432",
                 "91","82","73","721","64","631","541","532","4321",
                 "92","83","821","74","731","65","641","632","542","5321"
                 "93","921","84","831","75","741","732","651","642","6321","543","5421"};


/** The probabilities of each roll, 2 to 12. */
double probs[] =
{
    /*  0 */ 0.0,
    /*  1 */ 0.0,
    /*  2 */ 1.0 / 36.0,
    /*  3 */ 2.0 / 36.0,
    /*  4 */ 3.0 / 36.0,
    /*  5 */ 4.0 / 36.0,
    /*  6 */ 5.0 / 36.0,
    /*  7 */ 6.0 / 36.0,
    /*  8 */ 5.0 / 36.0,
    /*  9 */ 4.0 / 36.0,
    /* 10 */ 3.0 / 36.0,
    /* 11 */ 2.0 / 36.0,
    /* 12 */ 1.0 / 36.0
};


bool isin(string board, string mask)
{
    for (int i = 0; i < mask.size(); i++)
    {
        if (-1 == board.find(mask.at(i)))
        {
            return false;
        }
    }
    return true;
}

string remove(string board, string mask)
{
    string returnString = "";
    for (int i = 0; i < board.size(); i++)
    {
        if (-1 == mask.find(board.at(i)))
        {
            returnString += board.at(i);
        }
    }
    return returnString;
}

double findNext(string board)
{
    if ("" == board)
    {
        return 0.0;
    }

    for (int tot = MIN_DICE; tot <= MAX_DICE; tot++)
    {
        bool found = false;
        string mask = "123456789";
        int start = sitem[tot - 2];
        int end = eitem[tot - 2];

        for (int i = start; i < end; i++)
        {
            if (true == isin(board, sboard[i]))
            {
                mask = sboard[i];
                found = true;
                break;
            }
        }

        if (false == found)
        {
        }
        else
        {
            string newBoard = remove(board, mask);
            findNext(newBoard);
        }
    }
    return 0.0;
}

void firstMove(string board, int tot)
{
    bool found = false;
    string mask = "123456789";

    int start = sitem[tot-2];
    int end = eitem[tot-2];

    for (int i = start; i < end; i++)
    {
        if(true == isin(board, sboard[i]))
        {
            mask = sboard[i];
            found = true;
            break;
        }
    }

    if(false == found)
    {
        maxT += stoi(board);
        minT += stoi(board);
        return;
    }
    else
    {
        maxO = mask;
        string newBoard = remove(board, mask);
        findNext(newBoard);
    }
}

































bool maximize = false;
bool digits[MAX_NUM];
string taken;

bool isin2(string mask)
{
    for (int i = 0; i < mask.size(); i++)
    {
        if (true != digits[mask.at(i)-'0'])
        {
            return false;
        }
    }
    return true;
}

void remove2(string mask)
{
    for (int i = 0; i < mask.size(); i++)
    {
        digits[mask.at(i) - '0'] = false;
    }
}

void add2(string mask)
{
    for (int i = 0; i < mask.size(); i++)
    {
        digits[mask.at(i) - '0'] = true;
    }
}

int digitize(bool digits[])
{
    int total = 0;
    for (int i = 0; i < MAX_NUM; i++) 
    {
        if (digits[i])
        {
            total *= 10;
            total += i;
        }
    }
    return total;
}



double play(int roll, bool top)
{
    // We need an extreme, out-of-range value to start with.
    // That's different whether we're maximizing or minimizing
    double best = maximize ? -1.0 : 10000000000.0;

    int start = sitem[roll - 2];
    int end = eitem[roll - 2];

    for (int i = start; i < end; i++)
    {
        if (true == isin2(sboard[i]))
        {
    /*

    // Go through all digit combinations possible for this roll
    for (int takes[] : sums[roll])
    {
        // Are all of the digits available?
        bool cando = true;
        for (int take : takes) if (!digits[take]) cando = false;
        if (cando)
        {
*/
            // Keep playing another round
            double result = 0.0;
            remove2(sboard[i]);
            for (int r = 2; r <= 12; r++) result += probs[r] * play(r, false);
            add2(sboard[i]);

            // Is this better than anything we've seen so far?
            if (maximize ? result > best : result < best)
            {
                best = result;

                // We're only interested in saving the digits taken at the top level 
                if (top)
                {
                    taken = sboard[i];
                }
            }
        }
    }

    // If best is out of range, that means we couldn't play any combo of digits.
    // So, the result is just the digits we've got.
    double newthing = digitize(digits);
    return best < 0.0 || best>9999999999.0 ? newthing : best;
}


void solve(string remaining, int roll, bool max)
{
    maximize = max;
    
    for (int i = 0; i < MAX_NUM; i++)
    {
        digits[i] = false;
    }

    for (char digit : remaining)
    {
        digits[(int)(digit - '0')] = true;
    }
    taken = "-1";

    double best = 0.0;
    best = play(roll, true);

    cout << taken << std::showpoint << std::fixed << std::setprecision(5) << " " << best << endl;
}

int main() {
    string sboard;
    int d1 = 0;
    int d2 = 0;
    std::cin >> sboard >> d1 >> d2;
    int tot = d1+d2;

    solve(sboard, tot, false);
    solve(sboard, tot, true);

/*
    firstMove(sboard,tot);

    double min = (double)minT;
    double max = (double)maxT;

    cout << minO << std::showpoint << std::fixed << std::setprecision(5) << " " << min << endl;
    cout << maxO << std::showpoint << std::fixed << std::setprecision(5) << " " << max << endl;
*/
}