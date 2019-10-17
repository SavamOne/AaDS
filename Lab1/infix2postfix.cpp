#include "infix2postfix.h"

infix2postfix::infix2postfix(std::string& _infix) :
	infix(_infix)
{};

bool infix2postfix::is_operand(char ch)
{
	return isdigit(ch) || isalpha(ch);
}

bool infix2postfix::is_opening_bracket(char ch)
{
	return ch == '(';
}

bool infix2postfix::is_closing_bracket(char ch)
{
	return ch == ')';
}

bool infix2postfix::is_operator(char ch)
{
	return operators_priority(ch) != -1;//operators_priority.find(ch) != operators_priority.end();
}

short infix2postfix::operators_priority(char ch)
{
	switch (ch)
	{
	case '(':
		return 0;
	case '+':
		return 1;
	case '-':
		return 1;
	case '*':
		return 2;
	case '/':
		return 2;
	case '^':
		return 3;
	default:
		return -1;
	}

}

short infix2postfix::check_priority(char first, char second)
{
	short pr_first = operators_priority(first);//operators_priority.find(first)->second;
	short pr_second = operators_priority(second);//operators_priority.find(second)->second;
	if (pr_first > pr_second)
		return 1;
	else if (pr_first == pr_second)
		return 0;
	else
		return -1;
}

void infix2postfix::set(std::string& infix)
{
	this->infix = infix;
	postfix.clear();
	operators.clear();
}

std::string infix2postfix::get()
{
	return postfix;
}

std::string infix2postfix::convert()
{
	for (int i = 0; i < infix.size(); i++)
	{
		if (is_operand(infix[i]))
			postfix.push_back(infix[i]);

		else if (is_opening_bracket(infix[i]))
			operators.push(infix[i]);

		else if (is_closing_bracket(infix[i]))
		{
			char op = operators.pop();
			while (!is_opening_bracket(op))
			{
				postfix.push_back(' ');
				postfix.push_back(op);
				op = operators.pop();
			}
		}

		else if (is_operator(infix[i]))
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

	while (operators.size())
	{
		postfix.push_back(' ');
		postfix.push_back(operators.pop());
	}

	return postfix;
}