# 3bld-scrambler
 Scrambler program for Rubik's Cube blindfolded
 
## Introduction
This program generates custom scrambles for solving the rubik's cube blindfolded.  You can generate scrambles based on the number of algorithms required to solve the cube to practice solving easy or hard scrambles.  The number of algorithms is based on using the 3-Style method with UB and UBL buffers.  The formula for number of algorithms also assumes that 2 flipped edges can solved with one algorithm, and 2 twisted corners outside the buffer can be solved with one algorithm.
You can also generate scrambles with a specified number of flipped edges, or twisted corners.
Another feature allows you to gnereate different types of scrambles in fifferent distributions.

## Running the program
In order to run this program, you need Kociemba inistalled on your computer. `pip install kociemba`  
More inofrmation on Koceimba here: https://github.com/muodov/kociemba  
If you don't have Pip installed, go here: https://pip.pypa.io/en/stable/installing/  
Once the program is running, Specify the type of scramble you want (see below), click `scramble`, scramble your cube in your blindsolving orientation, and start solving! 

## Scramble Modes
### Number of algorithms
Enter the number of algorithms and the program will generate a scramble that requires that many algorithms to solve.
### Flipped Edges
Enter number of edges to be flipped, and specify whether the scramble is to have exactly that many edges flipped, or at least that many. 
The buffer is not included in the number.  If you specify 1, there will be 1 flipped edge outside the buffer requiring an algorithm to flip that edge and the buffer, or a cycle break.  If you specify 2, there  will be a floating 2-flip outside the buffer.
### Twisted Corners
Enter number of corners outside the buffer to be twisted, and specify whether the scramble is to have exactly that many twisted corners, or at least that many.  
If you specify `equal`, you can also specify if the twist is floating.  If you have 2 twisted corners and specify floating, then the corners will be twisted opposite directions so that after everything else is solved, the buffer will be solved and only the 2-twist will remain.  If you specify 2 corners non-floating, then the corners will be twisted the same direction, so that after solving the rest of the corners, the buffer will be twisted, resulting in a 3-twist.  If `Don't care` is selected for floating, then whether or not the twist is floating will be random.

## Custom Scrambles
This mode allows for distributions of different types of scrambles.  To use this mode, create a file defining what types of scrambles you want.  Each line of the file defines a particular type of scramble.

To specify a type of scramble based on number of algorithms:  
Format: `algs <number of algorithms> [<weight>]`  
Example: `algs 9` - Generates 9 algorithm scrambles

To specify a type of scramble based on the number of flipped edges:  
Format: `edges <number of flipped edges> [equal/atleast] [<weight>]`  If `equal/atleast` is not specified, `equal` will be used  
Example: `edges 2 atleast` - Generates scrambles with at least 2 flipped edges

To specify a type of scramble based on the number of twisted corners:  
Format: `corners <number of twisted corners> [equal/atleast] [floting/nonfloating] [<weight>]`  If `equal/atleast` is not specified, `equal` will be used.  if `floting/nonfloating` is not specified, then whether or not the twist is floating will be random  
Example: `corners 3 equal nonfloating` - Generates scrambles with 3 corners twisted outside the buffer, and the twist is not floating.

Weights are used to make some types of scrambles more common than others.  The default weight is 1. The file:  
```
algs 7 
algs 11
```
will generate scrambles such that half the scrambles require 7 algorithms to solve, and half of the scrambles require 11 algorithms to solve.   

The file:
```
algs 7 2 
algs 11 5
```
will generate scrambles such that 2/7 of the scrambles require 7 algorithms to solve, and 5/7 of the scrambles require 11 algorithms to solve.


## Coming Soon
- Configurable buffer pieces and parity swap pieces


