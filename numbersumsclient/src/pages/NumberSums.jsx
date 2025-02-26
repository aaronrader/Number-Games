import React, { useEffect, useReducer } from 'react';
import $ from "jquery"
import '../style/index.css';

import { serverUrl } from '../constants';
import { Box, Button, FormControl, InputLabel, MenuItem, Select, Switch, Typography } from '@mui/material';
import NumSumsBoard from '../components/NumSumsBoard';
import { useOutletContext } from 'react-router-dom';

export default function NumberSums(props) {
    const updateTitle = useOutletContext();
    const actions = Object.freeze({
        SELECT: 0,
        ERASE: 1
    });
    const counts = [4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

    const initialState = {
        currentBoard: {
            values: [],
            rowTotals: [],
            colTotals: [],
            rowCount: 8,
            colCount: 8,
        },
        clickAction: actions.SELECT,
        selectRows: 8,
        selectCols: 8,
        numIncorrect: 0
    }
    const reducer = (state, newState) => ({ ...state, ...newState });
    const [state, setState] = useReducer(reducer, initialState);

    useEffect(() => {
        updateTitle("Number Sums");
        genNewBoard();
    }, [])

    const genNewBoard = () => {
        //replace values with new
        $.get(`${serverUrl}/numSums?numRows=${state.selectRows}&numCols=${state.selectCols}`, function (data) {
            setState({
                currentBoard: {
                    values: data.values,
                    rowTotals: data.rowTotals,
                    colTotals: data.columnTotals,
                    rowCount: data.rowTotals.length,
                    colCount: data.columnTotals.length
                },
                numIncorrect: 0
            });

            //reset the board
            $(".correct").removeClass("correct");
            $(".wrong").removeClass("wrong");
            $(".cell").css("visibility", "visible");
            $("#gameOptions").toggle(250);
        });
    }

    const checkCell = (cell, e) => {
        if (state.clickAction === actions.SELECT && cell.isCorrect)
            $(e).addClass("correct");
        else if (state.clickAction === actions.ERASE && !cell.isCorrect)
            $(e).css("visibility", "hidden");
        else
            wrongSelection(e);
    }

    async function wrongSelection(e) {
        setState({numIncorrect: state.numIncorrect + 1})
        $(e).addClass("wrong");
        setTimeout(() => {
            $(e).removeClass("wrong");
        }, 250);
    }

    const onSwitchChange = (e) => {
        if ($(e.target).prop("checked"))
            setState({ clickAction: actions.ERASE });
        else
            setState({ clickAction: actions.SELECT });
    }

    return (
        <div className='gamePage'>
            <Box>
                <Button onClick={() => {$("#gameOptions").toggle(250);}} variant='outlined' sx={{marginBottom: "1vh"}}>New Game</Button>
                <Box id='gameOptions' sx={{display: "flex", alignItems: "center", padding: "1vmin", border: "1px solid #bbb", borderRadius: "0.5vw"}}>
                        <FormControl size='small' sx={{ margin: "1vh" }}>
                            <InputLabel id="rowSelectLabel">Rows</InputLabel>
                            <Select sx={{ minWidth: "10vw" }} labelId='rowSelectLabel' label="Rows" value={state.selectRows} onChange={(e) => { setState({ selectRows: e.target.value }) }}>
                                {counts.map((count) => (<MenuItem key={count} value={count}>{count}</MenuItem>))}
                            </Select>
                        </FormControl>
                        <FormControl size='small' sx={{ margin: "1vh" }}>
                            <InputLabel id="colSelectLabel">Columns</InputLabel>
                            <Select sx={{ minWidth: "10vw" }} labelId='colSelectLabel' label="Columns" value={state.selectCols} onChange={(e) => { setState({ selectCols: e.target.value }) }}>
                                {counts.map((count) => (<MenuItem key={count} value={count}>{count}</MenuItem>))}
                            </Select>
                        </FormControl>
                    <Button variant='contained' onClick={() => genNewBoard()}>Generate</Button>
                </Box>
            </Box>

            <Box>
                <NumSumsBoard board={state.currentBoard} onCellClicked={(rIndex, cIndex, e) => checkCell(state.currentBoard.values[rIndex][cIndex], e)} />
                <Box className='switchDiv'>
                    <Typography>SELECT</Typography>
                    <Switch onChange={onSwitchChange} />
                    <Typography>ERASE</Typography>
                </Box>
            </Box>

            <Box>
                <Typography>Mistakes: {state.numIncorrect}</Typography>
            </Box>
        </div>
    )
}