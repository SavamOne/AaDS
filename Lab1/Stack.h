#pragma once

//Стек на основе односвязного списка
template<typename T>
class Stack
{
	//Структура узла
	//содержит значение и указатель на следующий элемент
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
	//Метод добавления элемента
	void push(T value)
	{
		Element* elem = new Element(value, head); //Создать новый узел
		head = elem; //Сделать головой новосозданный элемнт
		size++;
	}

	//Метод вытаскивания элемента
	T pop()
	{
		if (size)
		{
			T value = head->value; //Взять значение
			Element* new_head = head->next; 
			delete head; //удалить голову
			head = new_head; //сделать головой следующий элемент удаленной головый
			size--;
			return value;
		}
		throw "There is no elements in stack";
	}

	//Метод просмотра элемента
	T peek()
	{
		if (size)
			return head->value;;
		throw "There is no elements in stack";
	}

	//Размер стека
	int size()
	{
		return size;
	}
	
	//Очитка стека
	//Удаляются все элементы, голова указывет на 0, размер равен 0
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

