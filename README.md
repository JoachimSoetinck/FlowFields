# FlowFields
## The Project
The Project consists out of a 2D World, where the player can spawn random agents. The player will also Be able to select to target where the agents need to go. The agents will move according to the folw field grid. The project will be made in Unity.

## What is a FlowField?

## Implementation
A flow field consist of a grid, a cost field , an integration field and an flow field.
Before implementing the Flow field, a grid is needed. The grid will exist out of different cells, which will store all the data of field.

### Cost Field
The Cost field will generate all the costS of a cell. The cost of a path can go between 1-254. How higher the cost, how rougher the area. The cost 255 is used to represent a wall. 0 will be the destination.

### Integration Field
The Integration field will calculate the best cost of the all the cells. This calculation will start from the destination point and will then add visit all the neigbors. 

### FlowField
For generating the Flowfield, all the nodes need to be checked to determine which direction there need to be pointed
