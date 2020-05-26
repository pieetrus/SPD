def read(nb):
    lines = []
    n = 0 # liczba operacji na maszynie w systemie
    m = 0 # ilość maszyn w systemie
    T = [] # tablica następników technologicznych
    M = [] # tablica następników maszynowych
    P = [] # tablica czasów wykonywania operacji P
    perm = [] # permutacje określające kolejność wykonywania operacji na maszynach
    with open('data\\data' + str(nb) + '.txt') as f:
        for i in range(0, 6):
            lines.append(f.readline())
        n = lines[0].split()
        T.append([int(x) for x in lines[1].split()])
        M.append([int(x) for x in lines[2].split()])
        P.append([int(x) for x in lines[3].split()])
        m = int(lines[4])
        temp = []
        for x in lines[5].split():
            if x != "0":
                temp.append(int(x))
            else:
                perm.append(list.copy(temp))
                temp.clear()

    return T, M, P, perm, m


def read_results(nb):
    with open('data\\out' + str(nb) + '.txt') as f:
        s = [] # lista momentów rozpoczęcia wykonywania operacji
        c = [] # lista momentów zakończenia wykonywania operacji
        c_max = 0 # czas realizacji wszystkich zadań
        for line in f:
            if line == "???": # zabezpieczenie przed crashem gdy w pliku nie ma odp
                return 0
            if not line.isspace():  # zabezpieczenie przed linią złożoną z samych białych znaków
                if " " in line:
                    start, end = line.split()
                    s.append(int(start))
                    c.append(int(end))
                else:
                    c_max = int(line)
        return s, c, c_max


if __name__ == '__main__':
    T, M, P, perm, m = read(5)
    print(perm)