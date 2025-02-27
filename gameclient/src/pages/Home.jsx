import React, { useEffect, useState } from 'react';
import $ from "jquery";
import { serverUrl } from '../constants';
import '../style/index.css';

import GameCard from '../components/GameCard';
import { useNavigate, useOutletContext } from 'react-router-dom';

export default function Home() {
    const updateTitle = useOutletContext();
    const navigate = useNavigate();

    const [gameList, setGameList] = useState([]);

    useEffect(() => {
        updateTitle("Home");
        $.get(`${serverUrl}/games`, function(data) {
            setGameList(data);
        });
    }, [])

    return (
        <div className='page'>
            <div className='gameGroup'>
                {gameList.map((game) => (
                    <GameCard onGameClick={() => navigate(game.endpoint)} gameData={game} key={game.name} />
                ))}
            </div>
        </div>
    );
}