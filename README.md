# GameProgramming2 Final
**Game Name:**	*Cable Connector!* (WIP)
	
**Description Short:**	Cable Connector is a singleplayer 2D puzzle game made in Unity that is based on PipeMania(1989). 
						It tests the player's ability to connect a given set of Cables from a starting point to an ending point. 
**Description Long:**	Based on PipeMania and the minigame-genre it established, Cable Connector is a singleplayer 2D puzzle game made in Unity where different shapes of cables must be connected to power a generator. 
			Types of cables include: A Straight Cable (Vertical or Horizontal), A Curved Cable (90 degree angle), a Resistor Cable (Slows Down Flow), and an Untouchable, Shorted Cable that can blow up. 
			With only a few Cables visible at the start of the level, the player must flip over tiles on the grid-based map in order to reveal the Cable they require.
					The player then needs to rotate their piece so that it fits properly with the previous tile's Power Output. 
					The Power Output is the Yellow electricity that runs through all of the cables it can go through. If the Power Output 
					is misdirected/ends up nowhere, the player will fail the level. 
					The player wins the level if cables can be used to get the Power Output flowing from the starting point to the ending point.
					
**Genre:** Puzzle
**Platform:** PC
**Folder Structure:**
\src
\src\packages
\assets
\docs
\build
\build\0.1POC
\build\0.2VS
\build\0.3Final

## Design:

- There is a 6-by-6 Grid that will hold each tile
- A Timer will countdown until the Power Output automatically begins
- There is a 6x6 Grid that will hold each tile
- A Timer will count down until the Power Output automatically begins
- Basic Instructions will be available to the player at all times

![Imgur](https://imgur.com/FsWffWT.png)
- A Cable knows where its "nodes"/ends are (Up,Down,Left,Right) - acknowledging whether its curved, straight, etc.
- A Cable can be told to "ReceivePower" (tell the cable which node-direction its receiving power from).
- A Cable keeps track of how long its held power since it first received it and updates its Sprite based on it.
- A Cable can "SendPower" to tell the LevelManager its End-Node Ouput.
- After the LevelManager gets info of where to send power to, it determines if the next tile in the given direction
 contains a cable and tells that cable to "ReceivePower".
## Art:
- All Cable Types contain 50 Keyframes of Power Output-fill Animations
- Sprites will be flipped/oriented just before taking Power Input to assure the animation plays correctly and begins flowing from the correct end
Straight-Cable/Pipe (Can be placed Vertically/Horizontally with any Rotation)
![Imgur](https://imgur.com/e7nD4Ks.gif)