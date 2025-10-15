import React, { useEffect, useState } from 'react';
import { View, StyleSheet, ScrollView, RefreshControl } from 'react-native';
import { Card, Text, Button, Avatar, ActivityIndicator } from 'react-native-paper';
import { useAuth } from '../contexts/AuthContext';
import { coupleService, CoupleResponse } from '../services/coupleService';

export default function DashboardScreen({ navigation }: any) {
  const { user, logout } = useAuth();
  const [couple, setCouple] = useState<CoupleResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);

  useEffect(() => {
    loadCouple();
  }, []);

  const loadCouple = async () => {
    try {
      const coupleData = await coupleService.getMyCouple();
      setCouple(coupleData);
    } catch (error) {
      console.error('Error loading couple:', error);
    } finally {
      setLoading(false);
      setRefreshing(false);
    }
  };

  const onRefresh = () => {
    setRefreshing(true);
    loadCouple();
  };

  const handleLogout = async () => {
    await logout();
  };

  if (loading) {
    return (
      <View style={styles.loadingContainer}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  const partner = couple?.members.find(m => m.userId !== user?.userId);

  return (
    <ScrollView
      style={styles.container}
      refreshControl={
        <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
      }
    >
      <View style={styles.header}>
        <Text variant="headlineMedium">Welcome, {user?.displayName}!</Text>
        <Text variant="bodyMedium" style={styles.subtitle}>
          {couple ? `Connected with ${partner?.displayName || 'Partner'}` : 'No couple yet'}
        </Text>
      </View>

      {couple && (
        <Card style={styles.card}>
          <Card.Content>
            <Text variant="titleMedium" style={styles.cardTitle}>
              Your Couple
            </Text>
            <View style={styles.coupleInfo}>
              <View style={styles.memberRow}>
                <Avatar.Text size={40} label={user?.displayName.charAt(0) || 'U'} />
                <View style={styles.memberInfo}>
                  <Text variant="bodyLarge">{user?.displayName}</Text>
                  <Text variant="bodySmall" style={styles.roleText}>You</Text>
                </View>
              </View>
              {partner && (
                <View style={styles.memberRow}>
                  <Avatar.Text size={40} label={partner.displayName.charAt(0)} />
                  <View style={styles.memberInfo}>
                    <Text variant="bodyLarge">{partner.displayName}</Text>
                    <Text variant="bodySmall" style={styles.roleText}>Partner</Text>
                  </View>
                </View>
              )}
            </View>
            <Text variant="bodySmall" style={styles.coupleCode}>
              Couple Code: {couple.code}
            </Text>
          </Card.Content>
        </Card>
      )}

      <Card style={styles.card}>
        <Card.Content>
          <Text variant="titleMedium" style={styles.cardTitle}>
            Quick Actions
          </Text>
          <Button
            mode="contained-tonal"
            onPress={() => {}}
            style={styles.actionButton}
            disabled
          >
            Track Mood (Coming Soon)
          </Button>
          <Button
            mode="contained-tonal"
            onPress={() => {}}
            style={styles.actionButton}
            disabled
          >
            Add Memory (Coming Soon)
          </Button>
          <Button
            mode="contained-tonal"
            onPress={() => {}}
            style={styles.actionButton}
            disabled
          >
            Likes & Dislikes (Coming Soon)
          </Button>
        </Card.Content>
      </Card>

      <Card style={styles.card}>
        <Card.Content>
          <Text variant="titleMedium" style={styles.cardTitle}>
            Account
          </Text>
          <Text variant="bodyMedium" style={styles.infoText}>
            Email: {user?.email}
          </Text>
          <Button
            mode="outlined"
            onPress={handleLogout}
            style={styles.logoutButton}
          >
            Logout
          </Button>
        </Card.Content>
      </Card>

      <View style={styles.footer}>
        <Text variant="bodySmall" style={styles.footerText}>
          Relationship App v1.0.0
        </Text>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  header: {
    padding: 24,
    backgroundColor: '#fff',
    borderBottomWidth: 1,
    borderBottomColor: '#e0e0e0',
  },
  subtitle: {
    marginTop: 4,
    opacity: 0.7,
  },
  card: {
    margin: 16,
    marginTop: 8,
    marginBottom: 8,
  },
  cardTitle: {
    marginBottom: 16,
  },
  coupleInfo: {
    marginTop: 8,
  },
  memberRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 12,
  },
  memberInfo: {
    marginLeft: 12,
  },
  roleText: {
    opacity: 0.6,
  },
  coupleCode: {
    marginTop: 12,
    padding: 8,
    backgroundColor: '#f5f5f5',
    borderRadius: 4,
    textAlign: 'center',
    fontFamily: 'monospace',
  },
  actionButton: {
    marginBottom: 8,
  },
  infoText: {
    marginBottom: 16,
  },
  logoutButton: {
    marginTop: 8,
  },
  footer: {
    padding: 24,
    alignItems: 'center',
  },
  footerText: {
    opacity: 0.5,
  },
});
