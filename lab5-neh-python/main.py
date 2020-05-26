def read(nb):
    data = []
    with open('data\\NEH' + str(nb) + '.dat') as f:
        n, m = f.readline().split()
        for i in f:
            if not i.isspace():  # zapezpieczenie przed linią złożoną z samych białych znaków
                data.append([int(x) for x in i.split()])
    return data, n, m

def read_wyniki(nb):
    with open('data\\NEH' + str(nb) + '.OUT') as f:
        return int(f.readline())


if __name__ == '__main__':
    lst = []
    for i in range(1, 10):
        lst.append(read(i))

    print(lst)



