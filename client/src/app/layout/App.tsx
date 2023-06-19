
import Catalog from "../../features/catalog/Catalog";
import {Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import Header from "./Header";

function App() {
 const theme = createTheme({
  palette:{
    mode: 'dark'
  }
 })

  return (
    <>
    <ThemeProvider theme={theme}>
    <CssBaseline/> {/*Removes margins on navbar*/}
      <Header/>
      <Container>
    <Catalog/>
    </Container>
    </ThemeProvider>
    </>
  );
}

export default App;
