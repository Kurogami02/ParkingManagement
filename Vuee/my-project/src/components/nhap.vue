<template>
  <div>
    <h3>Tìm Bãi Xe</h3>
    <p>Nhập ID:
      <input type="text" ref="ID">
      <input type="button" value="Search" @click="doGet">
    </p>
    <div v-if="ketqua">
      <!-- Hiển thị kết quả tìm kiếm -->
      <h3>Kết quả tìm kiếm:</h3>
      <p>ID: {{ ketqua.ID }}</p>
      <p>Danh mục: {{ ketqua.DanhMucID }}</p>
      <p>Tên xe: {{ ketqua.TenXe }}</p>
      <p>Vị Trí: {{ ketqua.ViTri }}</p>
      <p>Số lượng: {{ ketqua.SoLuong }}</p>
      <p>Hình ảnh: {{ ketqua.HinhAnh }}</p>
    </div>
    <ul v-if="errors && errors.length">
      <li v-for="error of errors">
        {{ error.message }}
      </li>
    </ul>
    <div class="Form1" style="padding: 25px;">
  
      <!-- Button để hiển thị form thêm mới -->
      <v-btn @click="showForm = !showForm" max-width="600px">Thêm thông tin</v-btn>
  
      <!-- Form thêm mới sinh viên -->
      <v-dialog v-model="showForm" max-width="600px">
        <v-card style="padding: 15px;">
          <h3>Thêm thông tin</h3>
          <v-text-field label="Danh mục" v-model="DanhMucID"></v-text-field>
          <v-text-field label="Tên xe" v-model="TenXe"></v-text-field>
          <v-text-field label="Vị trí" v-model="ViTri"></v-text-field>
          <v-text-field label=" Số lượng" v-model="SoLuong"></v-text-field>
          <v-text-field label="Hình ảnh" v-model="HinhAnh"></v-text-field>
          <v-btn @click="doPost" style="background-color: rgb(15, 238, 164);">Thêm mới</v-btn>
          <span>{{ message }}</span>
        </v-card>
      </v-dialog>
  
      <!-- Button để hiển thị form sửa thông tin -->
      <v-btn @click="showEditForm = !showEditForm">Sửa thông tin</v-btn>
  
      <!-- Form sửa thông tin sinh viên -->
      <v-dialog v-model="showEditForm" max-width="600px">
        <v-card style="padding: 15px;">
          <h3>Sửa thông tin</h3>
          <v-text-field label="ID" v-model="IDToEdit"></v-text-field>
          <v-text-field label="Danh mục" v-model="DanhMucIDToEdit"></v-text-field>
          <v-text-field label="Tên xe" v-model="TenXeToEdit"></v-text-field>
          <v-text-field label="Vị Trí" v-model="ViTriToEdit"></v-text-field>
          <v-text-field label="Số lượng" v-model="SoLuongToEdit"></v-text-field>
          <v-text-field label="Hình ảnh" v-model="HinhAnhToEdit"></v-text-field>
          <v-btn @click="doPut" style="background-color: rgb(15, 238, 164);">Cập nhật</v-btn>
          <span>{{ message }}</span>
        </v-card>
      </v-dialog> 
  
      <v-data-table v-if="showTable" :items="listBaiXe" :headers="headers"></v-data-table>
    </div>
    </div>
  </template>
  
    
    <script>
    import axios from 'axios';
    
    export default {
      data() {
        return {
          ketqua: "",
          errors: [],
          showForm: false,
          showEditForm: false,
          TenXe: "",
          ViTri: "",
          SoLuong: "",
          HinhAnh: "",
          DanhMucID: "",
          IDToEdit: "",
          DanhMucIDToEdit: "",
          TenXeToEdit: "",
          ViTriToEdit: "",
          SoLuongToEdit: "",
          HinhAnhToEdit: "",
          ID: "",
          message: "",
          listBaiXe: [],
          headers: [
            { text: 'ID', value: 'ID' },
            { text: 'Danh Mục', value: 'DanhMucID' },
            { text: 'Tên Xe', value: 'TenXe' },
            { text: 'Vị trí', value: 'ViTri' },
            { text: 'Số lượng', value: 'SoLuong' },
            { text: 'Hình ảnh', value: 'HinhAnh' }
          ]
        };
      },
      methods: {
        doGet(){
          axios.get('https://localhost:7117/api/BaiXe/' + this.$refs.ID.value)


           .then(response => {
            this.ketqua = response.data
            console.log(this.ketqua)
           })
           .catch(e => {
            this.errors.push(e)
           }) 
        },
        doPost() {
          axios.post('https://localhost:7117/api/BaiXe/', {
            DanhMucID: this.DanhMucID,
            TenXe: this.TenXe,
            ViTri: this.ViTri,
            SoLuong: this.SoLuong,
            HinhAnh: this.HinhAnh
          })
          .then(response => {
            this.message = "Thêm mới thành công!";
            // Cập nhật danh sách sinh viên sau khi thêm mới
          })
          .catch(e => {
            this.message = "Thêm mới thất bại!";
            console.error(e);
          });
        },
        doPut() {
          axios.put('https://localhost:7117/api/BaiXe/' + this.IDToEdit, {
    
            DanhMucID: this.DanhMucID,
            TenXe: this.TenXe,
            ViTri: this.ViTri,
            SoLuong: this.SoLuong,
            HinhAnh: this.HinhAnh
          })
          .then(response => {
            this.message = "Cập nhật thành công!";
            // Cập nhật danh sách sinh viên sau khi sửa thông tin
          })
          .catch(e => {
            this.message = "Cập nhật thất bại!";
            console.error(e);
          });
        },
        // Các phương thức khác để lấy và hiển thị danh sách sinh viên
      },
      // Mounted hoặc created hook để gọi API và lấy danh sách sinh viên
    };
    </script>