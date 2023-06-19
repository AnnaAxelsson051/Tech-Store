
import Catalog from "../../features/catalog/Catalog";
import {Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import Header from "./Header";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function App() {
  const [darkMode, setDarkMode] = useState(false);
  const paletteType = darkMode ? 'dark' : 'light';
 const theme = createTheme({
  palette:{
    mode: paletteType,
    background: {
       default: paletteType === 'light' ? '#ffffff' : '#121212'
}
  }
 })

 //Toggle for darkmode
 function handleThemeChange(){
  setDarkMode(!darkMode);
 }

  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored"/>
    <CssBaseline/> {/*Removes margins on navbar*/}
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
   <Outlet/>
    </Container>
    </ThemeProvider>
  );
}

export default App;
