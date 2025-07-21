# NatureOfCode-Unity
Working on writing the Nature of Code examples for Unity as a way to learn to script in Unity. 

Everything is derived from [The Nature of Code](https://natureofcode.com/introduction/) by Daniel Shiffman.

Organized into Chapters.  

Things I've learned so far when coming from Processing/P5.js:

The center of the canvas in Unity is 0,0,  whereas in Processing, the top left corner is 0,0

The camera controls the size of the ‘canvas’ 

For example, to create a 500x500 canvas in Processing, you would use the size function in Setup. However, in Unity, if you want an approximation of a 500x500 canvas, you would need to set the Camera to Orthographic and set its size to 250. This would give you a range of -250, 250 on the X and Y axies. You also need to make sure that the default camera has its ‘Y’ value set to 0. It defaults to 1. 

There are probably better ways to do everything that I am doing, so definitely don't trust everything I do. 

# Chapter 2

In chapter 2, I'm going to start doing a straight translation of the NOC examples as well as using the built-in physics engine of Unity, so there will be two versions of the exercises/examples
