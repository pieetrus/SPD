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

def read_wyniki(nb):
    with open('data\\wyniki' + str(nb) + '.txt') as f:
        return int(f.readline())

class Task:
    def __init__(self, nr, p, w, d):
        self.nr = nr # numer zadania - od 0
        self.p = p # czas wykonania zadania
        self.w = w # współczynnik kary za spóźnienie zadania
        self.d = d # żądany termin zakończenia zadania
    def __repr__(self):
        return "{0} {1} {2} {3}".format(self.nr, self.p, self.w, self.d)
        # return "{0}".format(self.nr)
    def __str__(self):
        return "{0} {1} {2} {3}".format(self.nr, self.p, self.w, self.d)
        # return "{0}".format(self.nr)

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
            t = C[i] - lst[i].d # policz spóźnienie
            F.append(t * lst[i].w) # pomnóż razy karę i dodaj je do listy
    return sum(F)

# ustawia bit na pozycji index, na wartość x(0/1), dla value
def modify_bit(value, index, x):
    mask = 1 << index
    return (value & ~mask) | ((x << index) & mask)

def calculate(I, lst, F):
    permutations_amount = bin(I).replace('0b', '') # liczba podzbiorów binarnie
    max_val_tab = []
    for j in range(len(permutations_amount)): # dla każdego elementu w permutacji
        modified = modify_bit(I, j, 0) # ustaw bit dla elementu j na 0
        modified_binary = bin(modified).replace('0b', '') # zamień reprezentacje zbioru na licznę binarną
        reversed_string = ''.join(reversed(permutations_amount)) # odwraca aby można było później zwrócić na których pozycjach bit jest ustawiony
        p = [pos for pos, char in enumerate(reversed_string) if char == '1'] # zwraca pozycje na których bit jest ustawiony
        if permutations_amount != modified_binary: # wykonuj dopóki nie sprawdzisz wszystkich ustawień (do stanu 1111itd.)
            time = 0
            for m in range(len(p)): # dla każdego zadania w tej permutacji
                time += lst[p[m]].p # policz czas wykonywania
            current_F = F[modified] # przypisz wartość funkcji celu dla danej permutacji z tablicy
            if current_F == -1: # jeśli jest równa -1(nie została jeszcze policzona), to ją policz
                current_F = calculate(modified, lst, F) # wylicz rekurencyjnie wartość funkcji celu dla danej permutacji
            max_val = max(time - lst[j].d, 0) * lst[j].w + current_F # wylicza funkcję celu włączając j zadanie
            max_val_tab.append(max_val)
    min_val = min(max_val_tab)
    F[I] = min_val
    return min_val


if __name__ == '__main__':
    wyniki_tab = []
    result_tab = []
    for file in range(1, 10):
        data = read(file)
        n = len(data)
        permutations = 2 ** n
        F = [0]
        for i in range(permutations - 1):
            F.append(-1)
        result = calculate(permutations - 1, data, F)
        result_tab.append(result)
        wyniki_tab.append(read_wyniki(file))

    print('Obliczonko: ')
    print(result_tab)
    print('Oczekiwane: ')
    print( wyniki_tab)




