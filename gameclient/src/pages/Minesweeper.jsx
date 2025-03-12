import React, { useEffect, useReducer, useState } from 'react';
import $ from "jquery";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, Switch, TextField, Typography } from '@mui/material';
import { useOutletContext } from 'react-router-dom';

import { serverUrl } from '../constants';
import MinesweeperBoard from '../components/MinesweeperBoard';

import '../style/index.css';
import '../style/gameBoard.css'

export default function Minesweeper(props) {
    /* CONSTANTS */
    const updateTitle = useOutletContext();
    const actions = Object.freeze({
        CLEAR: 0,
        FLAG: 1
    });

    /* STATE */
    const initialState = {
        currentBoard: {
            values: [],
            numBombs: 0,
            rowCount: 0,
            colCount: 0,
        },
        clickAction: actions.CLEAR,
        selectRows: 15,
        selectCols: 15,
        numCells: 0,
        numCleared: 0,
        numFlagged: 0
    }
    const reducer = (state, newState) => ({ ...state, ...newState });
    const [state, setState] = useReducer(reducer, initialState);
    const [modelOpen, setModelOpen] = useState(false);
    const [diaTitle, setDiaTitle] = useState("You win!");

    /* FUNCTIONS */
    useEffect(() => {
        updateTitle("Minesweeper");
        genNewBoard();
        $("#gameOptions").hide();
    }, []);

    useEffect(() => {
        //reset the board
        $(".cell").removeClass("flagged");
        $("#gameOptions").hide(250);
        setModelOpen(false);
    }, [state.currentBoard]);

    useEffect(() => {
        if (state.numCleared === (state.numCells - state.currentBoard.numBombs)) {
            showBoard();
            setDiaTitle("You win!");
            setModelOpen(true);
        }
    }, [state.numCleared]);

    const genNewBoard = () => {
        //replace values with new
        $.get(`${serverUrl}/minesweeper?numRows=${state.selectRows}&numCols=${state.selectCols}`, function (data) {
            setState({
                currentBoard: {
                    values: data.values,
                    numBombs: data.numBombs,
                    rowCount: data.values.length,
                    colCount: data.values[0].length
                },
                numCells: data.values.length * data.values[0].length,
                numCleared: 0,
                numFlagged: 0
            });
        });
    }

    const onSwitchChange = (e) => {
        if ($(e.target).prop("checked"))
            setState({ clickAction: actions.FLAG });
        else
            setState({ clickAction: actions.CLEAR });
    }

    const showBoard = () => {
        state.currentBoard.values.forEach(row => {
            row.forEach((cell) => {
                cell.isRevealed = true;
            })
        });
    }

    const checkCell = (cell, e) => {
        if (state.clickAction === actions.CLEAR && $(e).hasClass("flagged")) return; //user must unflag before clearing
        
        if (state.clickAction === actions.FLAG) { //flagged a tile
            if ($(e).hasClass("flagged")) {
                setState({numFlagged: state.numFlagged - 1});
                $(e).removeClass("flagged");
            }
            else {
                setState({numFlagged: state.numFlagged + 1});
                $(e).addClass("flagged");
            }

            return;
        }

        if (state.clickAction === actions.CLEAR && cell.isBomb) { //tried to clear a bomb
            showBoard();
            setDiaTitle("You lose!");
            setModelOpen(true);
            return;
        }
        
        if (state.clickAction === actions.CLEAR) { //cleared a spot
            //clear all adjacent non-bombs
            setState({numCleared: state.numCleared +  reveal(cell)});
        }
    }

    const reveal = (cell) => {
        if (cell.isRevealed) return 0;
        cell.isRevealed = true;

        if (cell.adjacent > 0) return 1;

        let numCleared = 1;

        if (cell.row !== 0 && cell.column !== 0)
            numCleared += reveal(state.currentBoard.values[cell.row - 1][cell.column - 1]);

        if (cell.row !== 0)
            numCleared += reveal(state.currentBoard.values[cell.row - 1][cell.column]);

        if (cell.row !== 0 && cell.column !== state.currentBoard.colCount - 1)
            numCleared += reveal(state.currentBoard.values[cell.row - 1][cell.column + 1]);

        if (cell.column !== 0)
            numCleared += reveal(state.currentBoard.values[cell.row][cell.column - 1]);

        if (cell.column !== state.currentBoard.colCount - 1)
            numCleared += reveal(state.currentBoard.values[cell.row][cell.column + 1]);

        if (cell.row !== state.currentBoard.rowCount - 1 && cell.column !== 0)
            numCleared += reveal(state.currentBoard.values[cell.row + 1][cell.column - 1]);

        if (cell.row !== state.currentBoard.rowCount - 1)
            numCleared += reveal(state.currentBoard.values[cell.row + 1][cell.column]);

        if (cell.row !== state.currentBoard.rowCount - 1 && cell.column !== state.currentBoard.colCount - 1)
            numCleared += reveal(state.currentBoard.values[cell.row + 1][cell.column + 1]);

        return numCleared;
    }

    return (
        <Box className="page">
            <Box>
                <Button onClick={() => {$("#gameOptions").toggle(250);}} variant='outlined' sx={{marginBottom: "1vh"}}>New Game</Button>
                <Box id='gameOptions' className='gameOptions'>
                        <FormControl size='small' sx={{ margin: "1vh" }}>
                            <TextField sx={{width: "6vw"}} type="number" label="Rows" value={state.selectRows} onChange={(e) => { setState({ selectRows: e.target.value }) }} />
                        </FormControl>
                        <FormControl size='small' sx={{ margin: "1vh" }}>
                            <TextField sx={{width: "6vw"}} type="number" label="Columns" value={state.selectCols} onChange={(e) => { setState({ selectCols: e.target.value }) }}/>
                        </FormControl>
                    <Button variant='contained' onClick={() => genNewBoard()}>Generate</Button>
                </Box>
            </Box>

            <Box className='game'>
                <MinesweeperBoard board={state.currentBoard} onCellClicked={(rIndex, cIndex, e) => checkCell(state.currentBoard.values[rIndex][cIndex], e)} />
                <Box className='switchDiv'>
                    <Typography>CLEAR</Typography>
                    <Switch onChange={onSwitchChange} />
                    <Typography>FLAG</Typography>
                </Box>
            </Box>

            <Box>
                <b>Bombs Left</b><br />
                {state.currentBoard.numBombs - state.numFlagged}
            </Box>

            <Dialog
                open={modelOpen}
                onClose={()=>{setModelOpen(false)}}
                sx={{top: "-75vh"}}
            >
                <DialogTitle sx={{textAlign: "center"}}>{diaTitle}</DialogTitle>
                <DialogContent>
                    <DialogActions>
                        <Button onClick={genNewBoard}>New Game</Button>
                    </DialogActions>
                </DialogContent>
            </Dialog>
        </Box>
    )
}