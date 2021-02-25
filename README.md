# Conway's Game of Life

## Description

A basic console application of Conway's Game of Life (GoL) written in C#.

## Usage

Initial configuration (seed) of the game can be either:
* a 'preset' loaded from a .txt file, or 
* a 'randomised' field of dead/alive cells. 

The first and subsequent generations are then determined using the following rules:
1. Any live cell with two or three neighbours survives.
2. Any dead cell with three live neighbours becomes a live cell.
3. All other live cells die in the next generation. All other dead cells stay dead.

As GoL is infinite, but the size of the program is finite, this lead to 'pathological edge effects'. Currently the program does not deal with this issue, but creating a buffer or wrapping is a future implementation.

* Add GIF/Images of the program running.

## Contributors

Code Reviews: Benjamin Sutas | https://github.com/LeftofZen

## References

* https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
