#pragma once 
template<typename T>
class Deque 
{
	struct Element
	{
		T value;
		Element* next;
		Element* prev;

		Element(T _value, Element* _next, Element* _prev) :
			value(_value),
			next(_next),
			prev(_prev)
		{}
	};

	int size = 0;
	Element *head = nullptr, *tail = nullptr;

public:
	void push_front(T value)
	{
		Element* elem = new Element(value, head, nullptr);
		if(head != nullptr) head->prev = elem;
		head = elem;
		if (size == 0) tail = head;
		size++;
	}

	void push_back(T value)
	{
		Element* elem = new Element(value, nullptr, tail);
		if(tail != nullptr) tail->next = elem;
		tail = elem;
		if (size == 0) head = tail;
		size++;
	}

	T pop_front()
	{
		if (size)
		{
			T value = head->value;
			if (size != 1)
			{
				Element* new_head = head->next;
				delete head;
				new_head->prev = nullptr;
				head = new_head;
				size--;
			}
			else
				clear();
			return value;
		}
		throw "There is no elements in deque";
	}

	T pop_back()
	{
		if (size)
		{
			T value = tail->value;
			if (size != 1)
			{
				Element* new_tail = tail->prev;
				delete tail;
				new_tail->next = nullptr;
				tail = new_tail;
				size--;
			}
			else
				clear();

			return value;
		}
		throw "There is no elements in deque";
	}

	T peek_front()
	{
		if (size)
			return head->value;
		throw "There is no elements in deque";
	}

	T peek_back()
	{
		if (size)
			return tail->value;
		throw "There is no elements in deque";
	}

	void clear()
	{
		while (head != nullptr)
		{
			Element* next = head->next;
			delete head;
			head = next;
		}
		head = tail = nullptr;
		size = 0;
	}

	~Deque()
	{
		clear();
	}
};

