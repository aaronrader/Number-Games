import React, { useState } from 'react';
import { Link, NavLink, Outlet } from "react-router-dom";
import './style/index.css';
import { AppBar, IconButton, Toolbar, Menu, MenuItem, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/MenuRounded'

export default function Layout() {
    const [anchorEl, setAnchorEl] = useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
      setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
      setAnchorEl(null);
    };
    
    const [title, setTitle] = useState("Aaron's Games");

    return (
        <div className="App">
            <AppBar className='header'>
                <Toolbar>
                    <Box id='logo'>
                        <NavLink to='/' style={({}) => ({textDecoration: "none"})}>
                            <h2>Number Games</h2>
                            <p>By Aaron Rader</p>
                        </NavLink>
                    </Box>
                    <h1>{title}</h1>
                    <IconButton
                        id='menuButton'
                        aria-controls={open ? 'pageMenu' : undefined}
                        aria-haspopup="true"
                        aria-expanded={open ? 'true' : undefined}
                        onClick={handleClick}
                        color='inherit'
                    >
                        <MenuIcon sx={{fontSize: 40}} />
                    </IconButton>
                    <Menu
                        id='pageMenu'
                        anchorEl={anchorEl}
                        open={open}
                        onClose={handleClose}
                    >
                        <MenuItem onClick={handleClose}><NavLink to="/">Home</NavLink></MenuItem>
                        <MenuItem onClick={handleClose}><NavLink to="/numsums">Number Sums</NavLink></MenuItem>
                        <MenuItem onClick={handleClose}><NavLink to="/minesweeper">Minesweeper</NavLink></MenuItem>
                    </Menu>
                </Toolbar>
            </AppBar>
            <Box className='content'>
                <Outlet context={setTitle}/>
            </Box>
            <AppBar className='footer'>
                <h4>Made by Aaron Rader</h4>
            </AppBar>
        </div>
    )
}