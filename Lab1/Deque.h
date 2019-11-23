#pragma once 

//Дека на основе двусвязного списка
template<typename T>
class Deque 
{
	//Структура узла
	//содержит значение и указатели на предыдущий и следующие узлы
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
	//Метод добавления элемента вперед
	void push_front(T value)
	{
		Element* elem = new Element(value, head, nullptr); //создать новый узел, указатель на следующий узел - голова списка, предыдущего узла нет
		if(head != nullptr) head->prev = elem; //Если существует голова, то предыдущим узлом для головы будет новосозданный узел
		head = elem; //головой становится этот элемент
		if (size == 0) tail = head; //если размер деки 1 узел, то хвост деки явялется тот же элемент, что голова
		size++; //увеличить счетчик размера деки
	}

	//Метод добавления элемента назад
	void push_back(T value)
	{
		Element* elem = new Element(value, nullptr, tail); //создать новый узел, указатель на следующий узел - ничего, предыдущего узла - хвост списка
		if(tail != nullptr) tail->next = elem; //Если существует хвоста, то предыдущим узлом для хвоста будет новосозданный узел
		tail = elem; //хвостом становится этот элемент
		if (size == 0) head = tail; //если размер деки 1 узел, то головой деки явялется тот же элемент, что хвост
		size++; //увеличить счетчик размера деки
	}

	//Метод вытаскивания элемента спереди
	T pop_front()
	{
		if (size)
		{
			T value = head->value;  //взять значение с головы
			if (size != 1) //если в деке больше 1 узлов, то головой сделать следующий узел, который был за прошлой головой
			{
				Element* new_head = head->next; 
				delete head;
				new_head->prev = nullptr;
				head = new_head;
				size--;
			}
			else //иначе вызвать метод clear()
				clear();
			return value;
		}
		throw "There is no elements in deque";
	}

	//Метод вытаскивания элемента сзади
	//Аналогично, как с методом выстаскивания элемента спереди, только испольуется хвост
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

	//Метод просмотра элемента спереди
	T peek_front()
	{
		if (size)
			return head->value; //Просто вернуть значение головы
		throw "There is no elements in deque";
	}

	//Метод просмотра элемента сзади
	T peek_back()
	{
		if (size)
			return tail->value; //Просто вернуть хвост головы
		throw "There is no elements in deque";
	}

	//Очистка деки
	//Удаляются все  элементы, указатели указывают на 0, размер равен 0
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

