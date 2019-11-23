#pragma once

//���� �� ������ ������������ ������
template<typename T>
class Stack
{
	//��������� ����
	//�������� �������� � ��������� �� ��������� �������
	struct Element
	{
		T value;
		Element* next;

		Element(T _value, Element* _next):
			value(_value),
			next(_next)
		{}
	};

	int size = 0;
	Element* head = nullptr;

public:
	//����� ���������� ��������
	void push(T value)
	{
		Element* elem = new Element(value, head); //������� ����� ����
		head = elem; //������� ������� ������������� ������
		size++;
	}

	//����� ������������ ��������
	T pop()
	{
		if (size)
		{
			T value = head->value; //����� ��������
			Element* new_head = head->next; 
			delete head; //������� ������
			head = new_head; //������� ������� ��������� ������� ��������� �������
			size--;
			return value;
		}
		throw "There is no elements in stack";
	}

	//����� ��������� ��������
	T peek()
	{
		if (size)
			return head->value;;
		throw "There is no elements in stack";
	}

	//������ �����
	int size()
	{
		return size;
	}
	
	//������ �����
	//��������� ��� ��������, ������ �������� �� 0, ������ ����� 0
	void clear()
	{
		while (head != nullptr)
		{
			Element* next = head->next;
			delete head;
			head = next;
		}
		head = nullptr;
		size = 0;
	}

	~Stack()
	{
		clear();
	}
};

