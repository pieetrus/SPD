'''[r,p,q]'''
from queue import PriorityQueue
import copy


class Holder:
    def __init__(self, vector_rpq):
        self.vector_rpq = vector_rpq

    def __eq__(self, other):
        return self.vector_rpq[2] == other.vector_rpq[2]

    def __lt__(self, other):
        return self.vector_rpq[2] > other.vector_rpq[2]

    def return_vector_rpq(self):
        return self.vector_rpq


def read(nb):
    lst = []
    with open('data\\Schrage' + str(nb) + '.DAT') as f:
        f.readline() #pominięcie n
        for i in f:
            x, y, z = i.split()
            lst.append([int(x), int(y), int(z)])
    return sorted(lst, key=lambda i: i[0]) # sortowanie po r


def schrage(N):
    N = sorted(N, key=lambda i: i[0])
    Cmax = t = b = i = 0
    G = PriorityQueue()
    Pi = []

    while len(N) or not G.empty():
        while len(N) and N[0][0] <= t:
            G.put(Holder(N[0]))
            del N[0]
        if G.empty():
            t = N[0][0]
            continue
        e = G.get().return_vector_rpq()
        t += e[1]
        if Cmax <= t + e[2]:
            Cmax = t + e[2]
            b = i
        i += 1
        Pi.append(e)
    return Pi, Cmax, b


def pre_schrage(N):
    N = sorted(N, key=lambda i: i[0])
    Cmax = t = 0
    G = PriorityQueue()
    l = [0, 0, 9999999]  # q0~=inf
    while len(N) or not G.empty():
        while len(N) and N[0][0] <= t:
            e = N[0]
            G.put(Holder(N[0]))
            del N[0]
            if e[2] > l[2]:
                l[1] = t - e[0]
                t = e[0]
                if l[1] > 0:
                    G.put(Holder(l))
        if G.empty():
            t = N[0][0]
            continue
        e = G.get().return_vector_rpq()
        l = e
        t += e[1]
        Cmax = max(Cmax, t + e[2])
    return Cmax


def findA(lst, b, Cmax):
    sum = a = 0
    for i in range(0, b + 1):
        sum += lst[i][1]

    while a < b and not Cmax == (lst[a][0] + lst[b][2] + sum):
        sum -= lst[a][1]
        a += 1
    return a


def findC(lst, a, b):
    for i in range(b - 1, a - 1, -1):
        if lst[i][2] < lst[b][2]:
            return i
    return None


def findRPQprim(lst, b, c):
    rprim = lst[c + 1][0]
    pprim = 0
    qprim = lst[c + 1][2]

    for i in range(c + 1, b + 1):
        if lst[i][0] < rprim:
            rprim = lst[i][0]
        if lst[i][2] < qprim:
            qprim = lst[i][2]
        pprim += lst[i][1]

    return rprim, pprim, qprim


def calier(lst):
    lst, Cmax, b = schrage(copy.deepcopy(lst))
    for i in lst:
        a = findA(lst, b, Cmax)
        c = findC(lst, a, b)
        if not c:
            return Cmax

        rprim, pprim, qprim = findRPQprim(lst, b, c)
        r_saved = lst[c][0]
        lst[c][0] = max(lst[c][0], rprim + pprim)
        LB = pre_schrage(copy.deepcopy(lst))

        if LB < Cmax:
            Cmax = min(Cmax, calier(lst))

        lst[c][0] = r_saved

        q_saved = lst[c][2]
        lst[c][2] = max(lst[c][2], qprim + pprim)
        LB = pre_schrage(copy.deepcopy(lst))

        if LB < Cmax:
            Cmax = min(Cmax, calier(lst))

        lst[c][2] = q_saved

    return Cmax


# if __name__ == "__main__":
#     # for i in range(1, 11):
#     #     lst = read("SCHRAGE" + str(i) + ".DAT")
#     #     val = calier(lst)
#     #     print(val)
#     lst = read("SCHRAGE" + str(10) + ".DAT")
#     val = calier(lst)
#     print(val)


xd = schrage(read(2))

print(xd)