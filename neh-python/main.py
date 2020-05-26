import sys

import numpy


def read(nb):
    tasks = []
    with open('data\\NEH' + str(nb) + '.dat') as f:
        n, m = f.readline().split()  # n - zadań, m - maszyn
        for i in f:
            if not i.isspace():  # zabezpieczenie przed linią złożoną z samych białych znaków
                tasks.append([int(x) for x in i.split()])
    return tasks, int(n), int(m)


def read_results(nb):
    with open('data\\NEH' + str(nb) + '.OUT') as f:
        return int(f.readline())


# j-zadanie k-maszyna
# n-zadania m-maszyny
def c_max(tasks, n, m):
    c = numpy.zeros((n+1, m+1))
    for j in range(1, n+1):
        for k in range(1, m+1):
            c[j][k] = max(c[j-1][k], c[j][k-1]) + tasks[j-1][k-1]
    return c


# sortuje listę zadań zgodnie z niemalejącą sumą czasów wykonania zadania
# na wszystkich maszynach
def sorted_by_p(tasks, n, m):
    permutation = []
    result = []
    for i in range(n):
        permutation.append(i)
    temp = sorted(permutation, key=lambda x: sum_p(tasks, x, m), reverse=True)
    for i in temp:
        result.append(tasks[i])
    return result


# zwraca sumę czasów wykonania zadania n na m maszynach
def sum_p(tasks, n, m):
    result = 0
    for i in range(m):
        result += tasks[n][i]
    return result


# n - zadań
# m - maszyn
def neh(tasks, n, m):
    tasks_lst = sorted_by_p(tasks, n, m)
    current_task = tasks_lst[0]
    sequence = [current_task]
    best_sequence = []

    for i in range(1, n): # dla każdego zadania
        best_c_max = sys.maxsize * 2 + 1 # bardzo duża liczba
        for j in range(0, i+1): # dla każdej pozycji w permutacji
            temp_sequence = list.copy(sequence)
            temp_sequence.insert(j, tasks_lst[i])
            n = len(temp_sequence)
            c_max_seq = c_max(temp_sequence, n, m)[n][m]
            if c_max_seq < best_c_max:
                best_sequence = temp_sequence
                best_c_max = c_max_seq
        sequence = best_sequence
    return int(best_c_max)


if __name__ == '__main__':
    lst = []

    for i in range(1, 10):
        tasks, n, m = read(i)

        print(str(i) + " - " + str(neh(tasks, n, m)) + " - " + str(read_results(i)))
