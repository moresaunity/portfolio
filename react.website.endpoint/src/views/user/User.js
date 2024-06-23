import React from 'react'
import { CTable, CTableHead, CTableRow, CTableHeaderCell, CTableDataCell, CTableBody, CButton, CSpinner } from '@coreui/react';
import axios from "axios";

const User = () => {

    const [users, setUsers] = React.useState([]);

  React.useEffect(() => {
    axios({
            mode: 'no-cors',
            headers: { 'Content-Type': 'application/json'},
             url: 'http://localhost:5168/api/v1/User',
          })
        .then(function (response) {
            setUsers(response.data.data);
        })
        .catch(function (error) {
            console.log(error);
        });
});
return (
    <>

      {users.length > 0 ? (
            <div style={{overflowX: 'auto'}}>
      <CTable>
      <CTableHead>
        <CTableRow>
          <CTableHeaderCell scope="col">#</CTableHeaderCell>
          <CTableHeaderCell scope="col">Image</CTableHeaderCell>
          <CTableHeaderCell scope="col">FullName</CTableHeaderCell>
          <CTableHeaderCell scope="col">PhoneNumber</CTableHeaderCell>
          <CTableHeaderCell scope="col">Email</CTableHeaderCell>
          <CTableHeaderCell scope="col">Roles</CTableHeaderCell>
        </CTableRow>
      </CTableHead>
      <CTableBody>
        {users.map((object, i) => (
          <CTableRow key={object.id}>
            <CTableHeaderCell scope="row">{object.id}</CTableHeaderCell>
            <CTableDataCell>
            <div className="avatar avatar-md"><a href="" target='_blank'><img src="" className="avatar-img" /></a><span className="avatar-status bg-success"></span></div>
            </CTableDataCell>
            <CTableDataCell>{object.fullName}</CTableDataCell>
            <CTableDataCell>{object.phoneNumber}</CTableDataCell>
            <CTableDataCell>{object.email}</CTableDataCell>            
            <CTableDataCell>
                {
                    object.roles.map((role, i) => (
                        <div class="small">{role}</div>
                ))}
            </CTableDataCell>
          </CTableRow>
        ))}
      </CTableBody>
    </CTable>
    </div>
    ) : (
<div className="text-center">
  <CSpinner size="sm" variant="grow" style={{ marginTop: "25%", width: '4rem', height: '4rem' }} />
</div>      
      )}
    </>
  );
};

export default User
