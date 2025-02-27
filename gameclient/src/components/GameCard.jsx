import React from "react";
import {Button} from "@mui/material"

import "../style/index.css"

const GameCard = (props) => {
    return (
        <Button className="gameDiv" variant="contained" onClick={() => props.onGameClick()}>
            {props.gameData.name}
        </Button>
    )
}

export default GameCard;