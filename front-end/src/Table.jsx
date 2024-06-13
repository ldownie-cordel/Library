import { useState, useCallback } from 'react';
import './Table.css'
export default function Table({tableData, nextPage, deleteFunc, addFunc, updateFunc, pageSize, setPageSize, pageNum}){
    const [editingId, setEditingId] = useState(null);
    const [editedBook, setEditedBook] = useState({});

    const startEditing = (book) => {
        setEditingId(book.id);
        setEditedBook({ author: book.author, title: book.title, pageCount: book.pageCount });
      };
    
      const cancelEditing = () => {
        setEditingId(null);
        setEditedBook({});
      };

      const handleUpdate = () => {
        updateFunc(editedBook, editingId);
        setEditingId(null);
        setEditedBook({});
      };

    return(
        <>
        <div className="Add">
            <button onClick={()=>{addFunc({ author: 'New Author', title: 'New Book', pageCount: 0 })}}>Add Book</button>
        </div>
        {(
         tableData?.data?.length > 0  ? 
            <div className="Table">
            <table>
                <tbody>
                <tr>
                    <th>Author</th>
                    <th>Title</th>
                    <th>Page Count</th>
                    <th>Functions</th>
                </tr>
                {tableData.data.map((val) => {
                    return (
                        <tr key={val.id}>
                             <td>
                    {editingId === val.id ? (
                      <input
                        type="text"
                        value={editedBook.author}
                        onChange={(e) => setEditedBook({ ...editedBook, author: e.target.value })}
                      />
                    ) : (
                      val.author
                    )}
                  </td>
                  <td>
                    {editingId === val.id ? (
                      <input
                        type="text"
                        value={editedBook.title}
                        onChange={(e) => setEditedBook({ ...editedBook, title: e.target.value })}
                      />
                    ) : (
                      val.title
                    )}
                  </td>
                  <td>
                    {editingId === val.id ? (
                      <input
                        type="number"
                        value={editedBook.pageCount}
                        onChange={(e) => setEditedBook({ ...editedBook, pageCount: parseInt(e.target.value) })}
                      />
                    ) : (
                      val.pageCount
                    )}
                  </td>
                            <td> 
                                <div className="tableFunctions">
                                {editingId === val.id ? (
                                <>
                                <button onClick={handleUpdate}>Save</button>
                                <button onClick={cancelEditing}>Cancel</button>
                                </>
                            ) : (
                                <>
                                <button onClick={() => startEditing(val)}>Edit</button>
                                <button onClick={() => deleteFunc(val.id)}>Delete</button>
                                </>
                            )}
                                    
                                </div>
                            </td>
                        </tr>
                    )
                })}

                </tbody>
            </table>
            <div className="pagination">
        <button onClick={() => nextPage(tableData.prev)} disabled={!tableData.prev}>Prev</button>
        <div className="size">
            <label htmlFor="pageSize">Page Size:</label>
            <input
                id="pageSize"
                type="number"
                value={pageSize}
                onChange={(e) => setPageSize(parseInt(e.target.value))}
            />
        </div>
        <button onClick={() => nextPage(tableData.next)} disabled={!tableData.next}>Next</button>
    </div>
    <div className="small-text">
        {tableData.next !== null ? tableData.next -1 : tableData.pages} of {tableData.pages}
    </div>
        </div>
        : "No Data To Load")}
        </>
    )
}