#pragma once

//Стек на основе динамического массива
template<typename T>
class Stack
{
	
	T* arr; //Сам массив для стека
	int arr_size; //Размер массива (на сколько элементов выделено памяти в массиве)
	int obj_count; //Количество объектов (сколько элементов массива занято)

	void reallocate(int how) //Перевыделение памяти для массива, когда он полностью заполняется/становится пустым
	{
		T* arr_new = new T[arr_size + how]; //Создать новый массив на how элементов разницы (how может быть как положительным, так и отрицательным)
		for (int i = 0; i < arr_size + (how < 0 ? how : 0); i++) //Заполнить этот новый массив элементами текущего массива
		{
			arr_new[i] = arr[i];
		}
		delete[] arr; //Удалить текущий массив
		arr = arr_new; //Поставить указатель на новосозданный массив
		arr_size += how; //поменять размер массива
	}

public:
	Stack() :
		obj_count(0),
		arr(new T[10]),
		arr_size(10)
	{}

	//Добавить элемент в стек
	void push(T value)
	{
		if (arr_size == obj_count) //Если массив заполнен, выделить память в массиве на +10 элемнтов
			reallocate(+10);
		arr[obj_count] = value; 
		obj_count++;
	}

	//Вытащить элемент из стека
	T pop()
	{
		if (obj_count)
		{
			T value = arr[obj_count - 1];  
			obj_count--;

			if (arr_size - 20 == obj_count) //Если количество элементов на 20 элементов меньше, чем размер массива, то уменьшить массив на 10 элементов
				reallocate(-10);

			return value;
		}
		throw "There is no elements in stack";
	}

	//Посмотреть элемент в стеке
	T peek()
	{
		if (obj_count)
			return arr[obj_count - 1];
		throw "There is no elements in stack";
	}

	//Размер стека
	int size()
	{
		return obj_count;
	}

	//Очистка стека
	void clear()
	{
		delete[] arr;
		arr_size = obj_count = 0;
	}

	~Stack()
	{
		clear();
	}
};

