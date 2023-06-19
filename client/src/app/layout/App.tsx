
import Catalog from "../../features/catalog/Catalog";
import {Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import Header from "./Header";
import { useState } from "react";

function App() {
  const [darkMode, setDarkMode] = useState(false);
  const paletteType = darkMode ? 'dark' : 'light';
 const theme = createTheme({
  palette:{
    mode: paletteType,
    background: {
       default: paletteType === 'light' ? '#f9f6f6' : '#121212'
}
  }
 })

 //Toggle for darkmode
 function handleThemeChange(){
  setDarkMode(!darkMode);
 }

  return (
    <>
    <ThemeProvider theme={theme}>
    <CssBaseline/> {/*Removes margins on navbar*/}
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
    <Catalog/>
    </Container>
    </ThemeProvider>
    </>
  );
}

export default App;
