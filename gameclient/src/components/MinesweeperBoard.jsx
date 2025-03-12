import React from 'react';
import TimerIcon from '@mui/icons-material/Timer'

import "../style/gameBoard.css"
import { Typography } from '@mui/material';

export default function MinesweeperBoard(props) {
    return (
        <div className='msTable'>
            <table>
                <tbody>
                    {props.board.values.map((row, rIndex) => (
                        <tr key={`row${rIndex}`}>
                            {row.map((value, cIndex) => (
                                value.isRevealed ?
                                    <td className='cell' key={`${rIndex}${cIndex}`} onClick={null}>
                                        {value.isBomb ?
                                            <TimerIcon className='bomb' key={`${rIndex}${cIndex}`} sx={{display: "flex", width: "100%", height: "100%"}}/> :
                                            <Typography className='value' sx={{lineHeight: "1", letterSpacing: "0", margin: "auto"}}>{value.adjacent > 0 ? value.adjacent : ''}</Typography>}
                                    </td>
                                : <td className='cell covered' key={`${rIndex}${cIndex}`} onClick={(e) => props.onCellClicked(rIndex, cIndex, e.target)}></td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}