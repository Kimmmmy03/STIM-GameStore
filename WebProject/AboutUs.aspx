<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="WebProject.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/AboutUs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Hero Section -->
    <div class="about-hero">
        <h1>Welcome to <span class="stim-brand">Stim</span></h1>
        <p>Empowering gamers. Connecting communities. Revolutionizing digital entertainment.</p>
    </div>


    <!-- About Description -->
    <div class="about-content">
        <div class="about-section">
            <h2>Our Story</h2>
            <p>
                Stim was founded with a simple mission: to create a space where gamers of all backgrounds can find
                their next favorite game. Built by a small team of passionate developers and gaming enthusiasts, we are
                committed to offering a curated selection of titles, fair prices, and a seamless user experience.
           
            </p>

        </div>

        <div class="about-section">
            <h2>Our Mission</h2>
            <p>
                To make high-quality games accessible to everyone and support indie developers by providing them a dedicated
                platform to showcase their work. We aim to bring value, excitement, and discovery into every purchase.
           
            </p>
        </div>

        <div class="about-section">
            <h2>Our Vision</h2>
            <p>
                We envision a future where gaming is more than entertainment — it becomes a shared culture, a global
                community, and a powerful tool for creativity and learning.
           
            </p>
        </div>

        <div class="about-section">
            <h2>Our Core Values</h2>
            <div class="about-values">
                <div class="value-card">
                    <h3>Innovation</h3>
                    <p>We embrace technology to deliver better gaming experiences every day.</p>
                </div>
                <div class="value-card">
                    <h3>Community</h3>
                    <p>We believe in the power of gamers coming together, sharing and growing.</p>
                </div>
                <div class="value-card">
                    <h3>Transparency</h3>
                    <p>Honest pricing and clear communication are at the heart of what we do.</p>
                </div>
                <div class="value-card">
                    <h3>Passion</h3>
                    <p>Gaming is our passion, and it fuels everything we build and improve.</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
