# Sokoban

Sokoban is a console-based game where the player moves around a grid, trying to get all the seeds into the storages.

## Features

- 100 Unique levels
- Ability to add more levels
- Ability to solo **playing**
- Self solving using various algorithms (DFS, BFS, UCS, A\*, HillClimbing)
- Ability to cancel algorithms execution using CancellationToken

## Preview

![level1](assets/image1.jpg)

---

![level11](assets/image2.jpg)

## Concept

Sokoban is a simple yet challenging puzzle game where you help a character move boxes around to the right spots. The word "Sokoban" in Japanese means "warehouse keeper," which fits perfectly with the concept of organizing boxes in a small space.

### How Does Sokoban Work?

1. Goal:

   - The goal is to move all the boxes onto their marked spots, or targets, to complete the level.

2. Rules:
   - You are allowed to push boxes but never pull them.
   - You can only push one box at a time.
   - You are not allowed to move through walls or push boxes into corners where they may get stuck.

### The Fun Factor?

It's not all about moving things around-you have to think!

Every move is crucial because if you push the box in the wrong direction, then you might block your way to the end of the level.
How each puzzle is solved is by planning ahead.

### Appearance:

It consists of a grid with some walls, several boxes, and marked target spots. Movement consists of pushing the boxes onto the target spots by moving your character up, down, left, or right.

    '@'	Player
    '+'	Player on storage
    '$'	Box
    '*'	Box on storage
    '.'	Storage
    '#'	Wall

### Controls:

- Arrow Keys: Movement
- N: Next level
- B: Previous level
- U: UnDo
- 1: Apply DFS algorithm
- 2: Apply BFS algorithm
- 3: Apply UCS algorithm
- 4: Apply A\* algorithm
- 5: Apply Hill-Climbing algorithm
- C: Cancel algorithm execution
- P: Show steps from the begging to the current state (Solve Path) step by step at the speed of 1 step every second
- L: Increase the speed of showing path
- K: Decrease the speed of showing path
- C: Cancel algorithm execution
- Q: Quit the game

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

- .NET 8 or higher

### Installing

A step by step series of examples that tell you how to get a development environment running

1. Clone the repository
1. Navigate to the project directory
1. Run `cd src`
1. Run `dotnet build` to build the project
1. Run `dotnet run` to start the game

## Built With

- [.NET Core](https://dotnet.microsoft.com/download)

If you like it give it a star ✨✨
