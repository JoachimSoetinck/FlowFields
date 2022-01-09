# FlowFields
## The Project
The Project consists out of a 2D Grid, where the player can spawn random agents. On the grid, the player will also bee able to select an end target where the agents need to go. The agents will move according to the flow field grid. This flow flow field will be generated. The project will be made in Unity.

## What is a FlowField?


## Implementation
A flow field consist of a grid, a cost field , an integration field and an flow field.
Before implementing the Flow field, a grid is needed. The grid will exist out of different cells, which will store all the data of field.

### Cost Field
The Cost field will generate all the cost of a cell. The cost of a path can go between 1-254. How higher the cost, how rougher the area. The cost 255 is used to represent a wall. 0 will be the destination. When the cost field is changed, the integration field and the flow field will need the recalculate their values.

### Integration Field
The Integration field will calculate the best cost of all the cells to the destination. This calculation will start from the destination point and will then add visit all the neigbors. The modified Dijksta algorithms will be used to determine all the path distance to the target. (calculation of the best cost to the destination). The main differce with the normal Dijkstra algorithms is that for a flowfield, The algorithms starts at the destination node and there is no start point.

### FlowField
Finally, for generating the flowfield. The grid takes all the result of the integration field and then tries to determine wich direction the agent should go. Each cell/node will look at its neighbors and compare their best cost. The direction that needs to be calculated, is that to the lowest best cost of one if the current cell/nodes neigbors.

## Sources
http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter24_Efficient_Crowd_Simulation_for_Mobile_Games.pdf
