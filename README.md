Conway's Game of Life

Description:
A basic implementation of Conway's Game of Life (GoL) using the console. 

Initial configuration (seed) can be either a 'preset' loaded from a text file, or 'randomised'. The program then uses the following rules to determine the next generation:

1) Any live cell with two or three neighbours survives.
2) Any dead cell with three live neighbours becomes a live cell.
3) All other live cells die in the next generation. All other dead cells stay dead.


As GoL is infinite, but the size of the program is finite, this lead to 'pathological edge effects'. Currently the program does not deal with this issue, but creating a buffer or wrapping is a future implementation.
