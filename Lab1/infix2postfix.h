#pragma once
#include <string>
#include "stack2.h"

class infix2postfix
{
	Stack<char> operators;

	std::string& infix;
	std::string postfix = "";

	bool is_operand(char ch);
	bool is_opening_bracket(char ch);
	bool is_closing_bracket(char ch);
	bool is_operator(char ch);
	short check_priority(char first, char second);
	short operators_priority(char ch);

public:
	infix2postfix(std::string& _infix);

	void set(std::string& infix);
	std::string get();

	std::string convert();
};

