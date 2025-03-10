import React, { useEffect, useReducer, useState } from 'react';
import $ from "jquery";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControl, InputLabel, MenuItem, Select, Switch, Typography } from '@mui/material';
import { useOutletContext } from 'react-router-dom';

import { serverUrl } from '../constants';
import NumSumsBoard from '../components/NumSumsBoard';

import '../style/index.css';

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
        clickAction: actions.SELECT,
        selectRows: 8,
        selectCols: 8,
        numCells: 64,
        numCleared: 0
    }
    const reducer = (state, newState) => ({ ...state, ...newState });
    const [state, setState] = useReducer(reducer, initialState);
    const [modelOpen, setModelOpen] = useState(false);

    /* FUNCTIONS */
    useEffect(() => {
        updateTitle("Minesweeper");
    }, []);
}