import React, { useEffect } from 'react';
import '../style/index.css';

import GameCard from '../components/GameCard';
import { useNavigate, useOutletContext } from 'react-router-dom';

export default function Home() {
    const updateTitle = useOutletContext();
    useEffect(() => {
        updateTitle("Home");
    }, [])

    const navigate = useNavigate();
    const games = [
        {
            name: "Number Sums",
            endpoint: "numSums"
        }
    ];

    const playGame = () => {
        navigate("/numsums");
    }

    return (
        <div className='gameGroup'>
            {games.map((game) => (
                <GameCard onGameClick={playGame} gameData={game} key={game.name} />
            ))}
        </div>
    );
}