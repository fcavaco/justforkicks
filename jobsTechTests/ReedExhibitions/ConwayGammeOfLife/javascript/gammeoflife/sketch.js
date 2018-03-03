function make2DArray(cols, rows){
    let arr = new Array(cols);
    for(let i = 0; i< arr.length; i++){
        arr[i]= new Array(rows);
    }
    return arr;
}

let grid;
let cols;
let rows;
let generations = 0;
let resolution = 10;
let canvas;
let canvasWidth = 600;
let canvasHeight = 400;

function setup(cycles){
    
    generations = cycles;

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
let currentCycle=0;
function computeNextGrid(cycle){
    let next = make2DArray(cols, rows);
    currentCycle =  cycle;
    for(let i = 0; i < cols; i++){
        for(let j = 0; j < rows; j++){

           // count number of live neighbors for each cell. neighbours who have 1.
           let neighbours = countNeighbours(grid, i, j);
           let state = grid[i][j]; 
           if(state == 0 && neighbours == 3){
               next[i][j] = 1;
           }
           else if(state == 1 && (neighbours < 2 || neighbours > 3)){
               next[i][j] = 0
           }
           else {
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

            let col = (i + x + cols) % cols;
            let row = (j + y + rows) % rows;
            sum +=grid[col][row];
        }    
    }
    sum -=grid[x][y];

    return sum;
}
function drawGrid(grid){
 
    for(let i = 0; i < cols; i++){
        for(let j = 0; j < rows; j++){
 
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