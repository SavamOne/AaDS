#pragma once
template<typename T>
class Stack
{
	T* arr;
	int arr_size;
	int obj_count;

	void reallocate(int how)
	{
		T* arr_new = new T[arr_size + how];
		for (int i = 0; i < arr_size + (how < 0 ? how : 0); i++)
		{
			arr_new[i] = arr[i];
		}
		delete[] arr;
		arr = arr_new;
		arr_size += how;
	}

public:
	Stack() :
		obj_count(0),
		arr(new T[10]),
		arr_size(10)
	{}

	void push(T value)
	{
		if (arr_size == obj_count)
			reallocate(+10);
		arr[obj_count] = value;
		obj_count++;
	}

	T pop()
	{
		if (obj_count)
		{
			T value = arr[obj_count - 1];
			obj_count--;

			if (arr_size - 20 == obj_count)
				reallocate(-10);

			return value;
		}
		throw "There is no elements in stack";
	}

	T peek()
	{
		if (obj_count)
			return arr[obj_count - 1];
		throw "There is no elements in stack";
	}

	int size()
	{
		return obj_count;
	}

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

