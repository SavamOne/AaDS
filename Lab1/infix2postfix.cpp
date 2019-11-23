#include "infix2postfix.h"

infix2postfix::infix2postfix(std::string& _infix) :
	infix(_infix)
{};

bool infix2postfix::is_operand(char ch) //Проверка на операнд
{
	return isdigit(ch) || isalpha(ch);
}

bool infix2postfix::is_opening_bracket(char ch) //Проверка на открывающую скобку
{
	return ch == '(';
}

bool infix2postfix::is_closing_bracket(char ch) //Проверка на закрывающую скобку
{
	return ch == ')';
}

bool infix2postfix::is_operator(char ch) //Проверка на оператор
{
	return operators_priority(ch) != -1;
}

short infix2postfix::operators_priority(char ch) //Приоритет операторов
{
	switch (ch)
	{
	case '(':
		return 0;
	case '+':
	case '-':
		return 1;
	case '*':
	case '/':
		return 2;
	case '^':
		return 3;
	default:
		return -1;
	}

}

short infix2postfix::check_priority(char first, char second) //Проверить приоритет операторов - если у первого выше, чем у второго - возвращается 1, если у второго - -1, если одинаково - 0
{
	short pr_first = operators_priority(first);
	short pr_second = operators_priority(second);
	if (pr_first > pr_second)
		return 1;
	else if (pr_first == pr_second)
		return 0;
	else
		return -1;
}

void infix2postfix::set(std::string& infix) {
	this->infix = infix;
	postfix.clear();
	operators.clear();
}

std::string infix2postfix::get()
{
	return postfix;
}

std::string infix2postfix::convert() //Основной метод перевода 
{
	for (int i = 0; i < infix.size(); i++) //Пройтись по всем символам
	{
		if (is_operand(infix[i])) //Если это операнд, то добавить его к финальному выражению
			postfix.push_back(infix[i]);

		else if (is_opening_bracket(infix[i])) //Если это открывающая скобка, то добавить ее в стек
			operators.push(infix[i]);

		else if (is_closing_bracket(infix[i])) //Если это закрывающая скобка, то то выташить из стека все элемента и поместить их к финальному выражению, пока не попадется открывающая скобка
		{
			char op = operators.pop();
			while (!is_opening_bracket(op))
			{
				postfix.push_back(' ');
				postfix.push_back(op);
				op = operators.pop();
			}
		}

		else if (is_operator(infix[i])) //Если это оператор, то добавить к финальному выражению все элементы из стека, которые по приоритету больше этого оператора, затем добавить этот оператор в стек
		{
			postfix.push_back(' ');
			while (operators.size() && check_priority(operators.peek(), infix[i]) >= 0)
			{
				postfix.push_back(operators.pop());
				postfix.push_back(' ');
			}
			operators.push(infix[i]);
		}
	}

	while (operators.size()) //После прохождения всех символов вытащить из стека все оставшиеся элементы
	{
		postfix.push_back(' ');
		postfix.push_back(operators.pop());
	}

	return postfix;
}