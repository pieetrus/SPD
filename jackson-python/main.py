data = open('dane/JACK3.DAT', 'r')

# pi(0) oraz c(0) = 0
n = int(data.readline().strip()) # liczba zadań
r = [0] # termin gotowości
p = [0] # czas wykonywania zadania
pi = [] # lista zadań
c = [0] # termin zakończenia

for line in data:
    line = line.split(' ')
    r.append(int(line[0]))
    p.append(int(line[1]))

for i in range(n+1):
    pi.append([r[i], p[i]])

pi.sort()

for i in range(1, n+1):
    temp = max(c[i-1], pi[i][0]) + pi[i][1]
    c.append(temp)

print(' ')
print('Permutacja optymalna {}'.format(pi))
print(' ')
print('Rozwiązanie optymalne Cmax: {}'.format(c[n]))


