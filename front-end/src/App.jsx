import { useState, useEffect, useCallback } from "react";
// import Header from './Header';
// import Footer from './Footer';
import Table from './Table';



function App() {
  const [method, setMethod] = useState("GET");
  const [tableData, setTableData] = useState({});
  const [pageNum, setPageNum] = useState(1);
  const [pageSize, setPageSize] = useState(4);

const getData = async() => {
  try{
    const details = await fetch(`http://localhost:3000/books?_page=${pageNum}&_per_page=${pageSize}`,
      {
        method: method,
      }
    )

    const books = await details.json();
    setTableData(books);
  }
  catch(e){
    console.log(e)
  }

}

const deleteData = async(id) => {
  try{
     await fetch(`http://localhost:3000/books/${id}`,
      {
        method: "DELETE",
      }
    )
    getData()
    console.log(pageNum)
  }
  catch(e){
    console.log(e)
  }

}

const addData = async(newBook) => {
  try{
     await fetch(`http://localhost:3000/books`,
      {
        method: "POST",
        body: JSON.stringify(newBook)
      }
    )
    getData()
  }
  catch(e){
    console.log(e)
  }

}

const updateData = async(updatedBook, bookId) => {
  try{
     await fetch(`http://localhost:3000/books/${bookId}`,
      {
        method: "PUT",
        body: JSON.stringify(updatedBook)
      }
    )
    getData()
  }
  catch(e){
    console.log(e)
  }

}

  useEffect(()=>{ // initial render
    getData()
  },[])

  useEffect(()=>{
    getData();
  },[pageNum, pageSize]) 
   

  return (
    <>
     {/* <Header/> */}
     <Table tableData={tableData} nextPage={setPageNum} deleteFunc={deleteData} addFunc={addData} updateFunc={updateData} pageSize={pageSize} setPageSize={setPageSize} pageNum={pageNum}/> 
    </>


  )
} 

export default App;
