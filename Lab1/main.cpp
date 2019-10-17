#include<iostream>
#include"infix2postfix.h"
#include"Deque.h"

using namespace std;



int main()
{
	string infix;
	getline(cin, infix);

	infix2postfix i2p(infix);

	cout << i2p.convert() << endl;

	//Deque<int> deque;

	//for (int i = 0; i < 100; i++)
	//{
	//	deque.push_front(i);
	//}

	//for (int i = 0; i < 101; i++)
	//{
	//	cout << deque.pop_back();
	//}

	system("pause");
}