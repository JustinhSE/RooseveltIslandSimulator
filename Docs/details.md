# Questions: First Stage

1. How to stitch together multiple cameras?
2. Measure Projector Angles
3. Mirrors
	-*Where should they go?*
	-*How do you address multiple screens?*
4. How can we test on the actual, life-sized screen?

| Description | Distance to Front / Middle Camera (meters) | Angle to Front / Middle Camera 
| ----------- | ----------- | ----------- |
| Left Camera | 0.85 | 148° |
| Right Camera | 0.61 | 138° |

## Process in Detail

Stitching together the cameras involved changing the viewport rect of the cameras to make sure the main screen was split into thirds (for the left, middle, and right outward cameras).

In order to find the distances between each camera in Unity, we had to first measure the actual distances between projectors and the screens in the physical driving simulator. We then wrote a C# script and used the update() function to continously output the distance and angle so we knew where to move it in the Unity scene (calculated using the Vector 3D Distance Formula).

The side mirrors and back mirror involved the use of scripts to activate all displays and invert the view(for the back mirror). It was necessary to experiment with the display numbers because it was not clear which display would render which camera on the real-life driving simulator.

