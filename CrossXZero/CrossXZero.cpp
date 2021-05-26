#include <iostream>
#include <random>
#include <stdlib.h>
#include <chrono>

using namespace std;

//=============================================================================================================

enum Cell 
{
    CROSS = 'X',
    ZERO = 'O',
    EMPTY = '_'
};

enum GameStatus 
{
    IN_PROGRESS,
    WON_HUMAN,
    WON_AI,
    DRAW
};

struct Field
{
    Cell human = EMPTY;
    Cell ai = EMPTY;
    Cell** ppField = nullptr;
    const size_t size = 3;
    size_t turn = 0;
    GameStatus status = IN_PROGRESS;
};

struct Coord 
{
    size_t y;
    size_t x;
};

//=============================================================================================================

int32_t getRandomNum(int32_t min, int32_t max)
{
    const static auto seed = chrono::system_clock::now().time_since_epoch().count();
    static mt19937_64 generator(seed);
    uniform_int_distribution<int32_t> dis(min, max);
    return dis(generator);
}

//=============================================================================================================

void InitGame(Field& f) 
{
    f.ppField = new Cell * [f.size];
    for (size_t y = 0; y < f.size; y++)
    {
        f.ppField[y] = new Cell[f.size];
    }

    for (size_t y = 0; y < f.size; y++)
    {
        for (size_t x = 0; x < f.size; x++)
        {
            f.ppField[y][x] = EMPTY;
        }
    }

    if (getRandomNum(0, 1000) > 500) 
    {
        f.human = CROSS;
        f.ai = ZERO;
        f.turn = 0;
    }
    else 
    {
        f.human = ZERO;
        f.ai = CROSS;
        f.turn = 1;
    }
}

void DeinitGame(Field &f) 
{
    for (size_t y = 0; y < f.size; y++)
    {
        delete[] f.ppField[y];
    }
    delete[] f.ppField;
}

void ClearField() 
{
    system("cls");
}

void DrawField(const Field &f) 
{   
    cout << endl << "      ";
    for (size_t x = 0; x < f.size; x++)
    {
        cout << x + 1 << "     ";
    }
    cout << endl;
    for (size_t y = 0; y < f.size; y++)
    {
        cout << " " << y + 1 << " |  ";
        for (size_t x = 0; x < f.size; x++)
        {
            cout << (char)f.ppField[y][x] << "  |  ";
        }
        cout << endl;
    }

    cout << endl << " Human: " << (char)f.human << endl << " Computer: " << (char)f.ai << endl;
};

//=============================================================================================================

int main()
{
    Field f;
    InitGame(f);
    ClearField();
    DrawField(f);


    //do {

    //}     
    //while ();


    DeinitGame(f);

    return 0;
}

