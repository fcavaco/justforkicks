
let grid;
let cols;
let rows;
let canvas;

let resolution = 10; // allows scalling the grid (number of little squares)
let canvasWidth = 600; // the horizontal size of the drawed grid
let canvasHeight = 400; // the vertical size of the drawed grid.

function setup(){
    
    canvas = createCanvas(canvasWidth, canvasHeight);
    cols = canvasWidth / resolution;
    rows = canvasHeight / resolution;

    grid = make2DArray(cols,rows);
    for(let i = 0; i < cols; i++){
        for(let j = 0; j < rows; j++){
            grid[i][j] = Math.floor(Math.random() * 2);
        }
    }
}

function createCanvas(width,height)
{
    canvas = document.getElementById('canvas');
    canvas.width = width;
    canvas.height = height;
    canvas.style.zIndex = 8;
    canvas.style.position = "absolute";
    canvas.style.border = "1px solid";
    canvas.style.background = "white";
    
    return canvas;
}
function rect(x,y,width,height, fill){

    // had to include setTimeout as I am running all of this in the browser. 
// (otherwise there would be no interruption to refresh the canvas and 
// we would just see the end result being rendered rather than animation.)

    setTimeout(function() {
        canvas = document.getElementById('canvas');
        var ctx = canvas.getContext("2d");
        ctx.fillStyle = fill;
        ctx.strokeStyle="black";
        ctx.strokeRect(x, y, width, height);
        ctx.fillRect(x, y, width, height);
        ctx.font="14px Georgia";
        
    }, 2);
}

function computeNextGrid(){
    let next = make2DArray(cols, rows);
    for(let i = 0; i < cols; i++){
        for(let j = 0; j < rows; j++){

           // count number of live neighbors for each cell. neighbours are those cells having state 1.
           let neighbours = countNeighbours(grid, i, j);
           
           // keep existing state for the cell : 1 or 0
           let state = grid[i][j]; 
           
           // apply Conway rules
           if(state == 0 && neighbours == 3){
               // will live
               next[i][j] = 1;
           }
           else if(state == 1 && (neighbours < 2 || neighbours > 3)){
               // will die
               next[i][j] = 0
           }
           else {
               // stasis
                next[i][j] = state;
           }
        }
    }
    grid = next;
}
function countNeighbours(grid, x, y){
    let sum = 0;
    for (let i = -1; i < 2; i++){
        for (let j = -1; j < 2; j++){
            // apply modulo to wrap around 2d grid (to the edge cells)
            let col = (i + x + cols) % cols;
            let row = (j + y + rows) % rows;
            sum +=grid[col][row];
        }    
    }

    // to discount the cell being evaluated itself.
    sum -=grid[x][y];

    return sum;
}

function drawGrid(grid){
 
    for(let i = 0; i < cols; i++){
        for(let j = 0; j < rows; j++){

            // resolution is defined in order to be able to scale out the frid.
            let x = i * resolution; 
            let y = j * resolution;

            if(grid[i][j] == 1)
            {
                rect(x,y,resolution,resolution, "black");
            }
            else{
                rect(x,y,resolution,resolution, "white");
            }
        }
    }
}

function make2DArray(cols, rows){
    let arr = new Array(cols);
    for(let i = 0; i< arr.length; i++){
        arr[i]= new Array(rows);
    }
    return arr;
}