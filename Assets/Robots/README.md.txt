ATONED ICARUS: ROBOT BASICS


_____________________
Drone Rundown:
_____________________

-Patrols between functionally as many waypoints as necessary.

-When the player walks underneath, the drone stops, shocks the player, and sends a signal for player death.

-If the taser is used on the drone, it stops in place and cannot shock the player until it is functional again. It is shown visibly when the red spotlight underneath the drone is deactivated.
NOTE: For the time being, the left mouse button is designated as the taser until the proper taser mechanic is integrated.

_____________________
Drone Setup:
_____________________

-Ensure that the floors and wall are properly set up for NavMeshAgent:
	-All walls have a NavMeshSurface component with the Default Area set to "Not Walkable".
	-The floor has a NavMeshSurface component with the Default Area set to "Walkable".
	-Press the Bake button on the floor, which should create a surface for the NavMeshAgent to automatically create a path between waypoints.

-Create however many waypoints you want it to reach. Make sure the Y-component is always set to 3.

-Under Controller (Script), open the Way Point menu, click + for each waypoint you created and add them in the newly created table.

-Drones detect the player based on the "Player" tag in the Arsenal Script, so it's done automatically as long as the player's tag is set to Player.


_____________________
Arsenal Rundown:
_____________________

-Stays in place, focusing a laser directly in front.

-When the player passes through the laser, they are killed.

-Arsenals have a radius around them in which they can listen to audio distractions. If one is played within said radius, the Arsenal turns toward the origin location (with the laser still active), and waits before turning back to its default direction.
NOTE: Q is the button to activate the temporary audio lure meant for testing, which will be different from how lures will operate in the final game.

-The inner half of the audio radius can also detect the player, if they step too close to the Arsenal it will automatically turn to the player unless they step away before the laser kills them.


_____________________
Arsenal Setup:
_____________________

-Under Arsenal Rotation (Script), adjust the Fov range as desired.
NOTE: Arsenal Script (Script) also has both Fov and Fov Angle, but this is for a separate sensor cone that should appear green. Do not use this cone.

-Also under Arsenal Rotation (Script), set the player model from the Hierarchy as the Player Target. This is used for switching between audio distractions and the player as the given target at any point.

