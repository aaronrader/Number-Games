import React from 'react';

import "../style/gameBoard.css"

export default function NumSumsBoard(props) {
    return (
        <div className='nsTable'>
            <table>
                <tbody>
                    <tr>
                        <td className='blank'></td>
                        {props.board.colTotals.map((total, index) => (
                            <td className='colTotals blank' key={`total${index}`}>{total}</td>
                        ))}
                    </tr>
                    {props.board.values.map((row, rIndex) => (
                        <tr key={`row${rIndex}`}>
                            <td className='rowTotals blank'>{props.board.rowTotals[rIndex]}</td>
                            {row.map((value, cIndex) => (
                                <td key={`${rIndex}${cIndex}`} className='cell' value={value} onClick={(e) => props.onCellClicked(rIndex, cIndex, e.target)}>{value.value}</td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}