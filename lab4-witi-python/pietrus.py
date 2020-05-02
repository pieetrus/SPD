def read(nb):
    lst = []
    counter = 0
    #lst.append(Task(0, 0, 0, 0)) # zerowe zadanie
    with open('data\\dane' + str(nb) + '.txt') as f:
        f.readline()  # pominięcie n
        for i in f:
            if not i.isspace():  # zapezpieczenie przed linią złożoną z samych białych znaków
                p, w, d = i.split()
                lst.append(Task(counter, int(p), int(w), int(d)))
                counter += 1
    return lst

class Task:
    def __init__(self, nr, p, w, d):
        self.nr = nr # numer zadania - od 0
        self.p = p # czas wykonania zadania
        self.w = w # współczynnik kary za spóźnienie zadania
        self.d = d # żądany termin zakończenia zadania
    def __repr__(self):
        # return "{0} {1} {2} {3}".format(self.nr, self.p, self.w, self.d)
        return "{0}".format(self.nr)
    def __str__(self):
        # return "{0} {1} {2} {3}".format(self.nr, self.p, self.w, self.d)
        return "{0}".format(self.nr)

class Permutations:
    perm = []

    def calculate(self, lst, left, right):
        if left == right:
            self.perm.append(lst)
            return lst
        for i in range(left, right + 1):
            swapped = lst.copy()
            self.swap(swapped, left, i)
            self.calculate(swapped, left + 1, right)

    def swap(self, lst, j, i):
        lst[j], lst[i] = lst[i], lst[j]

def punish(lst):
    n = len(lst) # ilość zadań
    temp = 0
    C = [] # czas zakończenia zadań
    F = [] # kara za spóźnienia
    for i in range(n):
        temp += lst[i].p
        C.append(temp)
    for i in range(n):
        if C[i] > lst[i].d: # jeżeli zadanie jest spóźnione
            t = C[i] - data[i].d # policz spóźnienie
            F.append(t * lst[i].w) # pomnóż razy karę i dodaj je do listy
    return sum(F)

if __name__ == '__main__':
    perm = []
    data = read(1)
    permutate = Permutations()
    permutate.calculate(data, 0, len(data)-1)
    for i in permutate.perm:
        print(punish(i))

